using System;
using System.ComponentModel;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            UserProfile();
            txtName.Text = UserName;
            txtEmail.Text = UserEmail;
            txtUsername.Text = UserLoggedIn;
            txtPassword.Text = "..........";
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty)
                txtEmail.Focus();
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtEmail.Text != string.Empty)
                txtUsername.Focus();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            providerCorrect.SetError(txtUsername, string.Empty);
            providerError.SetError(txtUsername, string.Empty);
            providerLoad.SetError(txtUsername, string.Empty);

            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, ResourceManager.GetString("cantBeEmpty"));
            else if (txtUsername.Text.Equals(UserLoggedIn))
                providerLoad.SetError(txtUsername, string.Empty);
            else
                providerLoad.SetError(txtUsername, ResourceManager.GetString("verifying"));
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != string.Empty)
                btnSave.PerformClick();
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            providerCorrect.SetError(txtUsername, string.Empty);
            providerError.SetError(txtUsername, string.Empty);
            providerLoad.SetError(txtUsername, string.Empty);

            if (UserSearch(txtUsername.Text) && txtUsername.Text != string.Empty && !txtUsername.Text.Equals(UserLoggedIn))
                providerCorrect.SetError(txtUsername, ResourceManager.GetString("available"));
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, ResourceManager.GetString("cantBeEmpty"));
                else if (txtUsername.Text.Equals(UserLoggedIn))
                    providerError.SetError(txtUsername, string.Empty);
                else
                    providerError.SetError(txtUsername, ResourceManager.GetString("unavailable"));
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            var isOpen = false;

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is frmProfilePass)
                {
                    frm.Focus();
                    isOpen = true;
                    break;
                }
            }

            if (!isOpen)
            {
                var profilepass = new frmProfilePass();
                profilepass.Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && (UserSearch(txtUsername.Text) || txtUsername.Text.Equals(UserLoggedIn)))
            {
                if (UserUpdate(txtName.Text, txtEmail.Text, txtUsername.Text))
                {
                    UserLoggedIn = txtUsername.Text;
                    Close();
                }
            }
            else
                MessageBox.Show(ResourceManager.GetString("fillFields"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
