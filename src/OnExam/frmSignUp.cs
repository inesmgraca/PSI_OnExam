using System;
using System.Windows.Forms;
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
                providerError.SetError(txtName, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
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
                providerError.SetError(txtEmail, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
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
            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
            else if (txtUsername.Text.Contains("-") || txtUsername.Text.Contains(" "))
                providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("invalidUsername"));
            else
                providerLoad.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("verifying"));
        }

        private void txtUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CheckUsername(txtUsername.Text) && txtUsername.Text != string.Empty && !txtUsername.Text.Contains("-") && !txtUsername.Text.Contains(" "))
                providerCorrect.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("available"));
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
                else if (txtUsername.Text.Contains("-") || txtUsername.Text.Contains(" "))
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("invalidUsername"));
                else
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("unavailable"));
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
                providerError.SetError(txtPassword, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
                providerError.SetError(txtConfirmPassword, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
            }
            else
            {
                providerError.SetError(txtPassword, Properties.Resources.ResourceManager.GetString("noMatch"));
                providerError.SetError(txtConfirmPassword, Properties.Resources.ResourceManager.GetString("noMatch"));
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
                btnSignUp_Click(sender, e);
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty && txtPassword.Text != string.Empty
                && CheckUsername(txtUsername.Text) && !txtUsername.Text.Contains("-") && !txtUsername.Text.Contains(" ") && txtPassword.Text.Equals(txtConfirmPassword.Text))
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
            {
                MessageBox.Show("Preencha todos os campos!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new frmLogin();
            login.Show();
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.mainForm.Show();
            Close();
        }

        private void frmSignUp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
