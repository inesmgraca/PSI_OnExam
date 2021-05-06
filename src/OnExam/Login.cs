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
                    UserLoggedIn = txtUsername.Text;
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
            MainForm.mainForm.Show();
            Close();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
