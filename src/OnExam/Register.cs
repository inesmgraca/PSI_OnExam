using System;
using System.Windows.Forms;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtNome.Text != "")
                txtEmail.Focus();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtEmail.Text != "")
                txtUsername.Focus();
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != "")
                txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPassword.Text != "")
                txtConfirmPassword.Focus();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != "" && txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != ""
                && txtConfirmPassword.Text != "" && txtPassword.Equals(txtConfirmPassword.Text))
            {
                if (UserRegister(txtNome.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text))
                {
                    var exams = new Exams();
                    exams.Show();
                    Close();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Close();
            MainForm.mainForm.Show();
        }

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
