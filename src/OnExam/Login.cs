using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != "")
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != "" && txtPassword.Text != "")
                btnLogin_Click(sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                if (UserLogin(txtUsername.Text, txtPassword.Text))
                {
                    //open gestão exames
                }
                else
                {
                    txtPassword.Text = "";
                }
            }
            else if (txtUsername.Text == "" && txtPassword.Text != "")
            {
                // erro
            }
            else if (txtUsername.Text != "" && txtPassword.Text == "")
            {
                // erro
            }
            else
            {
                // erro
            }

            txtUsername.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();
            Close();
        }
    }
}
