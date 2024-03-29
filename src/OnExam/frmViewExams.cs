﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.UserManagement;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmViewExams : Form
    {
        public frmViewExams()
        {
            InitializeComponent();
        }

        private void frmViewExams_Load(object sender, EventArgs e)
        {
            var ds = ExamsView();
            if (ds != null)
                dataGridExams.DataSource = ds.Tables["Exams"];
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var frms = new List<Form>();

            foreach (Form frm in Application.OpenForms)
            {
                if (!(frm is frmMain) && !(frm is frmViewExams))
                    frms.Add(frm);
            }
            
            foreach (var frm in frms)
                frm.Close();

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is frmMain)
                    frm.Show();
            }

            UserLoggedIn = string.Empty;
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmViewExams_Load(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var exam = new frmExam();
            exam.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridExams.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {
                int.TryParse(dataGridExams.SelectedRows[0].Cells["ExamID"].Value.ToString(), out int examID);

                if (MessageBox.Show(ResourceManager.GetString("verifyDelete"), ResourceManager.GetString("areYouSure"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (ExamDelete(examID))
                    {
                        MessageBox.Show(ResourceManager.GetString("deleteSuccess"), ResourceManager.GetString("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmViewExams_Load(sender, e);
                    }
                }
            }
            else
                MessageBox.Show(ResourceManager.GetString("oneExamOnly"), ResourceManager.GetString("selectedRows"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dataGridExams.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {
                var exam = new frmExam();
                exam.ExamID = (int)dataGridExams.SelectedRows[0].Cells["ExamID"].Value;
                exam.Show();
            }
            else
                MessageBox.Show(ResourceManager.GetString("oneExamOnly"), ResourceManager.GetString("selectedRows"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var isOpen = false;

            foreach (Form frm in Application.OpenForms)
            {
                if (frm is frmProfile)
                {
                    frm.Focus();
                    isOpen = true;
                    break;
                }
            }

            if (!isOpen)
            {
                var profile = new frmProfile();
                profile.Show();
            }
        }

        private void frmViewExams_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
