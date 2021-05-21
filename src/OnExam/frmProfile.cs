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
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
            UserGetProfile();
            txtName.Text = UserName;
            txtEmail.Text = UserEmail;
            txtUsername.Text = UserLoggedIn;
            txtPassword.Text = "..........";
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            providerCorrect.SetError(txtUsername, "");
            providerLoad.SetError(txtUsername, "");

            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, "Não pode estar vazio!");
            else if (txtUsername.Text.Equals(UserLoggedIn))
                providerLoad.SetError(txtUsername, "");
            else
                providerLoad.SetError(txtUsername, "...");
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            providerLoad.SetError(txtUsername, "");

            if (CheckUsername(txtUsername.Text) && txtUsername.Text != string.Empty && txtUsername.Text.Equals(UserLoggedIn))
            {
                providerError.SetError(txtUsername, "");
                providerCorrect.SetError(txtUsername, "Disponível!");
            }
            else
            {
                providerCorrect.SetError(txtUsername, "");

                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, "Não pode estar vazio!");
                else if (txtUsername.Text == UserLoggedIn)
                    providerError.SetError(txtUsername, "");
                else
                    providerError.SetError(txtUsername, "Indisponível!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && (CheckUsername(txtUsername.Text) || txtUsername.Text.Equals(UserLoggedIn)))
            {
                if (UserUpdateProfile(txtName.Text, txtEmail.Text, txtUsername.Text))
                {
                    UserLoggedIn = txtUsername.Text;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
