namespace SynFlooder {
    partial class Form1 {
        /// <summary>
        /// 是必需的设计师变数。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有使用中的资源。
        /// </summary>
        /// <param name="disposing">如果您要删除管理源，此true将会是false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form设计师的代码

        /// <summary>
        /// 帮助设计师的方法。
        /// 请不要将此方法修改为代码编辑器。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.IP_textbox = new MetroFramework.Controls.MetroTextBox();
            this.PORT_textbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RndSourceMac = new MetroFramework.Controls.MetroCheckBox();
            this.RndSourceIP = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.SOURCEIP_textbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.Data_textbox = new MetroFramework.Controls.MetroTextBox();
            this.SOURCEMAC_textbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.START_button = new MetroFramework.Controls.MetroButton();
            this.STOP_button = new MetroFramework.Controls.MetroButton();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.Sleep_textbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.TARGETMAC_textbox = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // IP_textbox
            // 
            this.IP_textbox.Location = new System.Drawing.Point(9, 57);
            this.IP_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.IP_textbox.Name = "IP_textbox";
            this.IP_textbox.Size = new System.Drawing.Size(124, 31);
            this.IP_textbox.TabIndex = 0;
            // 
            // PORT_textbox
            // 
            this.PORT_textbox.Location = new System.Drawing.Point(150, 57);
            this.PORT_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PORT_textbox.Name = "PORT_textbox";
            this.PORT_textbox.Size = new System.Drawing.Size(100, 31);
            this.PORT_textbox.TabIndex = 1;
            this.PORT_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnlyNumber);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(7, 23);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(71, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Ip Address";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(150, 23);
            this.metroLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(34, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Port";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TARGETMAC_textbox);
            this.groupBox1.Controls.Add(this.metroLabel10);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.metroLabel2);
            this.groupBox1.Controls.Add(this.IP_textbox);
            this.groupBox1.Controls.Add(this.PORT_textbox);
            this.groupBox1.Location = new System.Drawing.Point(141, 153);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(263, 177);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "攻击目标";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RndSourceMac);
            this.groupBox2.Controls.Add(this.RndSourceIP);
            this.groupBox2.Controls.Add(this.metroLabel6);
            this.groupBox2.Controls.Add(this.Data_textbox);
            this.groupBox2.Location = new System.Drawing.Point(25, 340);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(607, 260);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Etc..";
            // 
            // RndSourceMac
            // 
            this.RndSourceMac.AutoSize = true;
            this.RndSourceMac.Location = new System.Drawing.Point(116, 27);
            this.RndSourceMac.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RndSourceMac.Name = "RndSourceMac";
            this.RndSourceMac.Size = new System.Drawing.Size(128, 15);
            this.RndSourceMac.TabIndex = 7;
            this.RndSourceMac.Text = "随机源头MAC地址";
            this.RndSourceMac.UseVisualStyleBackColor = true;
            // 
            // RndSourceIP
            // 
            this.RndSourceIP.AutoSize = true;
            this.RndSourceIP.Location = new System.Drawing.Point(8, 27);
            this.RndSourceIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RndSourceIP.Name = "RndSourceIP";
            this.RndSourceIP.Size = new System.Drawing.Size(72, 15);
            this.RndSourceIP.TabIndex = 6;
            this.RndSourceIP.Text = "随机源IP";
            this.RndSourceIP.UseVisualStyleBackColor = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(0, 21);
            this.metroLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(64, 19);
            this.metroLabel3.TabIndex = 1;
            this.metroLabel3.Text = "Source IP";
            // 
            // SOURCEIP_textbox
            // 
            this.SOURCEIP_textbox.Location = new System.Drawing.Point(7, 55);
            this.SOURCEIP_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SOURCEIP_textbox.Name = "SOURCEIP_textbox";
            this.SOURCEIP_textbox.Size = new System.Drawing.Size(119, 31);
            this.SOURCEIP_textbox.TabIndex = 0;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(4, 51);
            this.metroLabel6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(39, 19);
            this.metroLabel6.TabIndex = 5;
            this.metroLabel6.Text = "DATA";
            // 
            // Data_textbox
            // 
            this.Data_textbox.Location = new System.Drawing.Point(4, 80);
            this.Data_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Data_textbox.Multiline = true;
            this.Data_textbox.Name = "Data_textbox";
            this.Data_textbox.Size = new System.Drawing.Size(603, 172);
            this.Data_textbox.TabIndex = 4;
            this.Data_textbox.Text = resources.GetString("Data_textbox.Text");
            // 
            // SOURCEMAC_textbox
            // 
            this.SOURCEMAC_textbox.Location = new System.Drawing.Point(8, 121);
            this.SOURCEMAC_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SOURCEMAC_textbox.Name = "SOURCEMAC_textbox";
            this.SOURCEMAC_textbox.Size = new System.Drawing.Size(118, 31);
            this.SOURCEMAC_textbox.TabIndex = 3;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(7, 92);
            this.metroLabel4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(74, 19);
            this.metroLabel4.TabIndex = 2;
            this.metroLabel4.Text = "SourceMac";
            // 
            // START_button
            // 
            this.START_button.Location = new System.Drawing.Point(33, 607);
            this.START_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.START_button.Name = "START_button";
            this.START_button.Size = new System.Drawing.Size(247, 109);
            this.START_button.TabIndex = 6;
            this.START_button.Text = "START";
            this.START_button.Click += new System.EventHandler(this.START_button_Click);
            // 
            // STOP_button
            // 
            this.STOP_button.Location = new System.Drawing.Point(373, 607);
            this.STOP_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.STOP_button.Name = "STOP_button";
            this.STOP_button.Size = new System.Drawing.Size(247, 109);
            this.STOP_button.TabIndex = 7;
            this.STOP_button.Text = "STOP";
            this.STOP_button.Click += new System.EventHandler(this.STOP_button_Click);
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(29, 108);
            this.metroComboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(603, 29);
            this.metroComboBox1.TabIndex = 8;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(29, 80);
            this.metroLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(37, 19);
            this.metroLabel5.TabIndex = 9;
            this.metroLabel5.Text = "网卡";
            // 
            // Sleep_textbox
            // 
            this.Sleep_textbox.Location = new System.Drawing.Point(29, 210);
            this.Sleep_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Sleep_textbox.Name = "Sleep_textbox";
            this.Sleep_textbox.Size = new System.Drawing.Size(100, 31);
            this.Sleep_textbox.TabIndex = 10;
            this.Sleep_textbox.Text = "10";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(29, 176);
            this.metroLabel7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(62, 19);
            this.metroLabel7.TabIndex = 11;
            this.metroLabel7.Text = "休眠(ms)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.metroLabel8);
            this.groupBox3.Controls.Add(this.metroLabel9);
            this.groupBox3.Controls.Add(this.metroLabel3);
            this.groupBox3.Controls.Add(this.SOURCEMAC_textbox);
            this.groupBox3.Controls.Add(this.SOURCEIP_textbox);
            this.groupBox3.Controls.Add(this.metroLabel4);
            this.groupBox3.Location = new System.Drawing.Point(412, 155);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(220, 177);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "虚假源地址";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(7, 23);
            this.metroLabel8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(0, 0);
            this.metroLabel8.TabIndex = 2;
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(7, 92);
            this.metroLabel9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(0, 0);
            this.metroLabel9.TabIndex = 3;
            // 
            // TARGETMAC_textbox
            // 
            this.TARGETMAC_textbox.Location = new System.Drawing.Point(9, 123);
            this.TARGETMAC_textbox.Margin = new System.Windows.Forms.Padding(4);
            this.TARGETMAC_textbox.Name = "TARGETMAC_textbox";
            this.TARGETMAC_textbox.Size = new System.Drawing.Size(124, 31);
            this.TARGETMAC_textbox.TabIndex = 5;
            this.TARGETMAC_textbox.Text = "02:02:02:02:02:02";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(8, 94);
            this.metroLabel10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(70, 19);
            this.metroLabel10.TabIndex = 4;
            this.metroLabel10.Text = "TargetMac";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 726);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.Sleep_textbox);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroComboBox1);
            this.Controls.Add(this.STOP_button);
            this.Controls.Add(this.START_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(23, 80, 23, 27);
            this.Resizable = false;
            this.Text = "SYN洪泛攻击实验工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox IP_textbox;
        private MetroFramework.Controls.MetroTextBox PORT_textbox;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroTextBox SOURCEMAC_textbox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox SOURCEIP_textbox;
        private MetroFramework.Controls.MetroButton START_button;
        private MetroFramework.Controls.MetroButton STOP_button;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox Data_textbox;
        private MetroFramework.Controls.MetroCheckBox RndSourceMac;
        private MetroFramework.Controls.MetroCheckBox RndSourceIP;
        private MetroFramework.Controls.MetroTextBox Sleep_textbox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private System.Windows.Forms.GroupBox groupBox3;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroTextBox TARGETMAC_textbox;
        private MetroFramework.Controls.MetroLabel metroLabel10;
    }
}

