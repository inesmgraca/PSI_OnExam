using System;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class frmSignUp : Form
    {
        public frmSignUp()
        {
            InitializeComponent();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty)
                txtEmail.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text == string.Empty)
                providerError.SetError(txtName, ResourceManager.GetString("cantBeEmpty"));
            else
                providerError.SetError(txtName, string.Empty);
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtEmail.Text != string.Empty)
                txtUsername.Focus();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text == string.Empty)
                providerError.SetError(txtEmail, ResourceManager.GetString("cantBeEmpty"));
            else
                providerError.SetError(txtEmail, string.Empty);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != string.Empty)
                txtPassword.Focus();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            providerCorrect.SetError(txtUsername, string.Empty);
            providerError.SetError(txtUsername, string.Empty);
            providerLoad.SetError(txtUsername, string.Empty);

            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, ResourceManager.GetString("cantBeEmpty"));
            else if (txtUsername.Text.Contains("-") || txtUsername.Text.Contains(" "))
                providerError.SetError(txtUsername, ResourceManager.GetString("invalidUsername"));
            else
                providerLoad.SetError(txtUsername, ResourceManager.GetString("verifying"));
        }

        private void txtUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            providerCorrect.SetError(txtUsername, string.Empty);
            providerError.SetError(txtUsername, string.Empty);
            providerLoad.SetError(txtUsername, string.Empty);

            if (UserSearch(txtUsername.Text) && txtUsername.Text != string.Empty && !txtUsername.Text.Contains("-") && !txtUsername.Text.Contains(" "))
                providerCorrect.SetError(txtUsername, ResourceManager.GetString("available"));
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, ResourceManager.GetString("cantBeEmpty"));
                else if (txtUsername.Text.Contains("-") || txtUsername.Text.Contains(" "))
                    providerError.SetError(txtUsername, ResourceManager.GetString("invalidUsername"));
                else
                    providerError.SetError(txtUsername, ResourceManager.GetString("unavailable"));
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPassword.Text != string.Empty)
                txtConfirmPassword.Focus();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtConfirmPassword.Text) && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
            {
                providerError.SetError(txtPassword, string.Empty);
                providerError.SetError(txtConfirmPassword, string.Empty);
            }
            else if (txtPassword.Text == string.Empty && txtConfirmPassword.Text == string.Empty)
            {
                providerError.SetError(txtPassword, ResourceManager.GetString("cantBeEmpty"));
                providerError.SetError(txtConfirmPassword, ResourceManager.GetString("cantBeEmpty"));
            }
            else
            {
                providerError.SetError(txtPassword, ResourceManager.GetString("noMatch"));
                providerError.SetError(txtConfirmPassword, ResourceManager.GetString("noMatch"));
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
                btnSignUp.PerformClick();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty && txtPassword.Text != string.Empty
                && !txtUsername.Text.Contains("-") && !txtUsername.Text.Contains(" ") && txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                if (UserSignUp(txtName.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text))
                {
                    UserLoggedIn = txtUsername.Text;
                    var exams = new frmViewExams();
                    exams.Show();
                    Close();
                }
            }
            else
                MessageBox.Show("Preencha todos os campos!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new frmLogin();
            login.Show();
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is frmMain)
                    frm.Show();
            }

            Close();
        }

        private void frmSignUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
