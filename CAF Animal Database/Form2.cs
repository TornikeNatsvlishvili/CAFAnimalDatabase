using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace CAF_Animal_Database
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbSubject.SelectedIndex = 0;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!txtFrom.Text.Equals("") && !txtMessage.Text.Equals(""))
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("patron@CAFANIMALDATABASE.com");
                message.To.Add(new MailAddress("tornikeDev@gmail.com"));
                message.Subject = "From: " + txtFrom.Text + " | CAF ANIMAL DATABASE: " + cmbSubject.Text;
                message.Body = txtMessage.Text;
                SmtpClient client = new SmtpClient();
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("testEmailCSharpCred", "12345678as")
                };
                try
                {
                    smtp.Send(message);
                    MessageBox.Show("Message Sent Sucessfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Make sure you fill in all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }
    }
}
