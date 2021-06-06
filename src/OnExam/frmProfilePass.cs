using System;
using System.Windows.Forms;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class frmProfilePass : Form
    {
        public frmProfilePass()
        {
            InitializeComponent();
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
            if (e.KeyCode == Keys.Enter && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
                btnSave_Click(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text != string.Empty && txtPassword.Text.Equals(txtConfirmPassword.Text))
            {
                if (UserUpdatePass(txtPassword.Text))
                    Close();
            }
        }

        private void frmProfilePass_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
