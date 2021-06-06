﻿using System;
using System.Windows.Forms;
using static OnExam.TakeExamManagement;

namespace OnExam
{
    public partial class frmTakeExamInfo : Form
    {
        public frmTakeExamInfo()
        {
            InitializeComponent();
        }

        private void txtExamName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtExamName.Text != string.Empty)
                txtName.Focus();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtName.Text != string.Empty)
                txtInfo.Focus();
        }

        private void btnTakeExam_Click(object sender, EventArgs e)
        {
            if (txtExamName.Text != string.Empty && txtName.Text != string.Empty && txtInfo.Text != string.Empty)
            {
                if (txtExamName.Text.Contains("-"))
                {
                    var i = txtExamName.Text.IndexOf("-");
                    var examOwner = txtExamName.Text.Substring(0, i).Trim();
                    var examName = txtExamName.Text.Substring(i + 1).Trim();

                    if (TakeExam(0, examName, examOwner))
                    {
                        if (SessionAdd(txtName.Text.Trim(), txtInfo.Text.Trim()))
                        {
                            var takeExam = new frmTakeExam();
                            takeExam.Show();
                            Close();
                        }
                    }
                }
                else if (int.TryParse(txtExamName.Text.Trim(), out int ID))
                {
                    if (TakeExam(ID, "", "%"))
                    {
                        if (SessionAdd(txtName.Text.Trim(), txtInfo.Text.Trim()))
                        {
                            var takeExam = new frmTakeExam();
                            takeExam.Show();
                            Close();
                        }
                    }
                }
                else
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("invalidExam"), Properties.Resources.ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void frmTakeExamInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
