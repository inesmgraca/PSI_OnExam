using System;
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
            if (e.KeyCode == Keys.Enter && txtUsername.Text != string.Empty)
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != string.Empty && txtPassword.Text != string.Empty)
                btnLogin_Click(sender, e);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text != string.Empty && txtPassword.Text != string.Empty)
            {
                if (UserLogin(txtUsername.Text, txtPassword.Text))
                {
                    var exams = new Exams();
                    exams.Show();
                    Close();
                }
                else
                {
                    txtPassword.Text = string.Empty;
                }
            }
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, "Não pode estar vazio!");

                if (txtPassword.Text == string.Empty)
                    providerError.SetError(txtPassword, "Não pode estar vazio!");
            }

            txtUsername.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.mainForm.Show();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }
    }
}
