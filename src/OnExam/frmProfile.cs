﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
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
            if (txtUsername.Text == string.Empty)
                providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
            else if (txtUsername.Text.Equals(UserLoggedIn))
                providerLoad.SetError(txtUsername, string.Empty);
            else
                providerLoad.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("verifying"));
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtUsername.Text != string.Empty)
                btnSave_Click(sender, e);
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            if (CheckUsername(txtUsername.Text) && txtUsername.Text != string.Empty && !txtUsername.Text.Equals(UserLoggedIn))
            {
                providerCorrect.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("available"));
            }
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
                else if (txtUsername.Text.Equals(UserLoggedIn))
                    providerError.SetError(txtUsername, string.Empty);
                else
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("unavailable"));
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            var profilePass = new frmProfilePass();
            profilePass.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != string.Empty && txtEmail.Text != string.Empty && txtUsername.Text != string.Empty
                && (CheckUsername(txtUsername.Text) || txtUsername.Text.Equals(UserLoggedIn)))
            {
                if (UserUpdate(txtName.Text, txtEmail.Text, txtUsername.Text))
                {
                    UserLoggedIn = txtUsername.Text;
                    Close();
                }
            }
            else
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("fillFields"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
