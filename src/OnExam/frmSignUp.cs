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
            if (e.KeyCode == Keys.Enter && txtName.Text != "")
                txtEmail.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
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

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text == string.Empty)
                providerError.SetError(txtEmail, "Não pode estar vazio!");
            else
                providerError.SetError(txtEmail, "");
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != "")
                txtPassword.Focus();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            providerCorrect.SetError(txtUsername, "");
            providerLoad.SetError(txtUsername, "");

            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, "Não pode estar vazio!");
            else
                providerLoad.SetError(txtUsername, "...");
        }

        private void txtUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            providerLoad.SetError(txtUsername, "");

            if (CheckUsername(txtUsername.Text) && txtUsername.Text != string.Empty)
            {
                providerError.SetError(txtUsername, "");
                providerCorrect.SetError(txtUsername, "Disponível!");
            }
            else
            {
                providerCorrect.SetError(txtUsername, "");

                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, "Não pode estar vazio!");
                else
                    providerError.SetError(txtUsername, "Indisponível!");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtPassword.Text != "")
                txtConfirmPassword.Focus();
        }

        private void Password_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals(txtConfirmPassword.Text) && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
            {
                providerError.SetError(txtPassword, "");
                providerError.SetError(txtConfirmPassword, "");
            }
            else if (txtPassword.Text == string.Empty && txtConfirmPassword.Text == string.Empty)
            {
                providerError.SetError(txtPassword, "Não pode estar vazio!");
                providerError.SetError(txtConfirmPassword, "Não pode estar vazio!");
            }
            else
            {
                providerError.SetError(txtPassword, "Password não coincide!");
                providerError.SetError(txtConfirmPassword, "Password não coincide!");
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && txtPassword.Text != string.Empty && txtConfirmPassword.Text != string.Empty)
                btnRegister_Click(sender, e);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty && txtPassword.Text != string.Empty
                && txtConfirmPassword.Text != string.Empty && CheckUsername(txtUsername.Text) && txtPassword.Text.Equals(txtConfirmPassword.Text))
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

        private void Register_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
