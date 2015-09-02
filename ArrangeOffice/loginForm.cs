using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace ArrangeOffice
{
    public partial class loginForm : Form
    {
        private ArrangeOffice form;
        public loginForm()
        {
            InitializeComponent();
        }

        public void pushForm(ArrangeOffice form) 
        {
            this.form = form;
  
        
        }

        public void login(string account, string password)
        {
            MD5 md5Hash = MD5.Create();

            byte[] pass = Encoding.UTF8.GetBytes(password.ToString());
            pass = md5Hash.ComputeHash(pass);
            string md5Password = "";
            for (int i = 0; i < pass.Length; i++)
            {
                md5Password += pass[i].ToString("x2");
            }
            form.send("LOGIN\tMASTER\t" + account + "\t" + md5Password + "<END>");

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            login(accountTextBox.Text, passwordTextBox.Text);
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
         //   this.Close();
            form.close();
        }



     
    }
}
