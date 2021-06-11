using System;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.SessionManagement;

namespace OnExam
{
    public partial class frmTakeExam : Form
    {
        public frmTakeExam()
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
                            var session = new frmSession();
                            session.Show();
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
                            var session = new frmSession();
                            session.Show();
                            Close();
                        }
                    }
                }
                else
                    MessageBox.Show(ResourceManager.GetString("invalidExam"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void frmTakeExam_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
