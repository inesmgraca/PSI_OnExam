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

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != "")
                txtEmail.Focus();
        }

        private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtName.Text == string.Empty)
                providerError.SetError(txtName, "Não pode estar vazio!");
            else
                providerError.SetError(txtName, "");
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
            if (txtName.Text != "" && txtEmail.Text != "" && txtUsername.Text != "" && txtPassword.Text != ""
                && txtConfirmPassword.Text != "" && txtPassword.Equals(txtConfirmPassword.Text))
            {
                if (UserRegister(txtName.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text))
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
            //if (e.CloseReason == CloseReason.)
            //Application.Exit();
        }
    }
}
