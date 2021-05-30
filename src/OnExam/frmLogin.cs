﻿using System;
using System.Windows.Forms;
using static OnExam.UserManagement;

namespace OnExam
{
    public partial class frmLogin : Form
    {
        public frmLogin()
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
                    var exams = new frmViewExams();
                    exams.Show();
                    Close();
                }
                else
                    txtPassword.Text = string.Empty;
            }
            else
            {
                if (txtUsername.Text == string.Empty)
                    providerError.SetError(txtUsername, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));

                if (txtPassword.Text == string.Empty)
                    providerError.SetError(txtPassword, Properties.Resources.ResourceManager.GetString("cantBeEmpty"));
            }

            txtUsername.Focus();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var signup = new frmSignUp();
            signup.Show();
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
