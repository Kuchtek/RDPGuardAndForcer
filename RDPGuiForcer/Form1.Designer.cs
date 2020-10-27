namespace RDPGuiForcer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rdp = new AxMSTSCLib.AxMsRdpClient9NotSafeForScripting();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordsUsed = new System.Windows.Forms.ComboBox();
            this.ErrorLog = new System.Windows.Forms.Label();
            this.ThreadsNum = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Generated = new System.Windows.Forms.CheckBox();
            this.PlusButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.Attack = new System.Windows.Forms.Button();
            this.ServerNameBox = new System.Windows.Forms.TextBox();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.MaskedTextBox();
            this.Password = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.ServerName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rdp);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.PasswordsUsed);
            this.splitContainer1.Panel2.Controls.Add(this.ErrorLog);
            this.splitContainer1.Panel2.Controls.Add(this.ThreadsNum);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.Generated);
            this.splitContainer1.Panel2.Controls.Add(this.PlusButton);
            this.splitContainer1.Panel2.Controls.Add(this.StopButton);
            this.splitContainer1.Panel2.Controls.Add(this.Attack);
            this.splitContainer1.Panel2.Controls.Add(this.ServerNameBox);
            this.splitContainer1.Panel2.Controls.Add(this.UsernameBox);
            this.splitContainer1.Panel2.Controls.Add(this.PasswordBox);
            this.splitContainer1.Panel2.Controls.Add(this.Password);
            this.splitContainer1.Panel2.Controls.Add(this.UserName);
            this.splitContainer1.Panel2.Controls.Add(this.ServerName);
            this.splitContainer1.Size = new System.Drawing.Size(1005, 635);
            this.splitContainer1.SplitterDistance = 606;
            this.splitContainer1.TabIndex = 0;
            // 
            // rdp
            // 
            this.rdp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdp.Enabled = true;
            this.rdp.Location = new System.Drawing.Point(0, 0);
            this.rdp.Name = "rdp";
            this.rdp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdp.OcxState")));
            this.rdp.Size = new System.Drawing.Size(1005, 606);
            this.rdp.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Użyte hasła";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(411, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "\\";
            // 
            // PasswordsUsed
            // 
            this.PasswordsUsed.FormattingEnabled = true;
            this.PasswordsUsed.Location = new System.Drawing.Point(529, 62);
            this.PasswordsUsed.Name = "PasswordsUsed";
            this.PasswordsUsed.Size = new System.Drawing.Size(154, 21);
            this.PasswordsUsed.TabIndex = 9;
            // 
            // ErrorLog
            // 
            this.ErrorLog.AutoSize = true;
            this.ErrorLog.Location = new System.Drawing.Point(58, 118);
            this.ErrorLog.Name = "ErrorLog";
            this.ErrorLog.Size = new System.Drawing.Size(0, 13);
            this.ErrorLog.TabIndex = 13;
            // 
            // ThreadsNum
            // 
            this.ThreadsNum.FormattingEnabled = true;
            this.ThreadsNum.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.ThreadsNum.Location = new System.Drawing.Point(125, 80);
            this.ThreadsNum.Name = "ThreadsNum";
            this.ThreadsNum.Size = new System.Drawing.Size(121, 21);
            this.ThreadsNum.TabIndex = 8;
            this.ThreadsNum.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Ilość wątków";
            // 
            // Generated
            // 
            this.Generated.AutoSize = true;
            this.Generated.Location = new System.Drawing.Point(58, 62);
            this.Generated.Name = "Generated";
            this.Generated.Size = new System.Drawing.Size(188, 17);
            this.Generated.TabIndex = 7;
            this.Generated.Text = "Hasło generowane automatycznie";
            this.Generated.UseVisualStyleBackColor = true;
            this.Generated.CheckedChanged += new System.EventHandler(this.Generated_CheckedChanged);
            // 
            // PlusButton
            // 
            this.PlusButton.Location = new System.Drawing.Point(930, 2);
            this.PlusButton.Name = "PlusButton";
            this.PlusButton.Size = new System.Drawing.Size(25, 23);
            this.PlusButton.TabIndex = 6;
            this.PlusButton.Text = "+";
            this.PlusButton.UseVisualStyleBackColor = true;
            this.PlusButton.Click += new System.EventHandler(this.PlusButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(842, 1);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 5;
            this.StopButton.Text = "Stop!";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Attack
            // 
            this.Attack.Location = new System.Drawing.Point(716, -1);
            this.Attack.Name = "Attack";
            this.Attack.Size = new System.Drawing.Size(75, 23);
            this.Attack.TabIndex = 4;
            this.Attack.Text = "Atakuj!";
            this.Attack.UseVisualStyleBackColor = true;
            this.Attack.Click += new System.EventHandler(this.Attack_Click);
            // 
            // ServerNameBox
            // 
            this.ServerNameBox.Location = new System.Drawing.Point(58, 2);
            this.ServerNameBox.Name = "ServerNameBox";
            this.ServerNameBox.Size = new System.Drawing.Size(162, 20);
            this.ServerNameBox.TabIndex = 1;
            // 
            // UsernameBox
            // 
            this.UsernameBox.Location = new System.Drawing.Point(334, 3);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(146, 20);
            this.UsernameBox.TabIndex = 2;
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(529, 2);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(154, 20);
            this.PasswordBox.TabIndex = 3;
            // 
            // Password
            // 
            this.Password.AutoSize = true;
            this.Password.Location = new System.Drawing.Point(486, 3);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(36, 13);
            this.Password.TabIndex = 2;
            this.Password.Text = "Hasło";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(226, 3);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(102, 13);
            this.UserName.TabIndex = 1;
            this.UserName.Text = "Nazwa użytkownika";
            // 
            // ServerName
            // 
            this.ServerName.AutoSize = true;
            this.ServerName.Location = new System.Drawing.Point(12, 3);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(40, 13);
            this.ServerName.TabIndex = 0;
            this.ServerName.Text = "Serwer";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 635);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button Attack;
        private System.Windows.Forms.TextBox ServerNameBox;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.MaskedTextBox PasswordBox;
        private System.Windows.Forms.Label Password;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.Label ServerName;
        private System.Windows.Forms.Button PlusButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Generated;
        private System.Windows.Forms.ComboBox ThreadsNum;
        private System.Windows.Forms.Label ErrorLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox PasswordsUsed;
        private AxMSTSCLib.AxMsRdpClient9NotSafeForScripting rdp;
    }
}

