using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace SynFlooder {
    static class Program {
        /// <summary>
        /// 是这些应用程序的主要来源。
        /// </summary>
        [STAThread]
        static void Main() {

            if (!IsAdministrator()) {
                try {
                    MessageBox.Show("未以管理员权限执行。\n开放请求管理权限的提升.");
                    ProcessStartInfo procInfo = new ProcessStartInfo();
                    procInfo.UseShellExecute = true;
                    procInfo.FileName = Application.ExecutablePath;
                    procInfo.WorkingDirectory = Environment.CurrentDirectory;
                    procInfo.Verb = "runas";
                    Process.Start(procInfo);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                    return;
                }
            } else {
                MessageBox.Show("SYN洪泛攻击实现工具\n用于实验用途\n请勿用于非法用途\n制作人员:王智峰-201858080113");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
        public static bool IsAdministrator() {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (identity != null) {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}
