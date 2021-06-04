using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                dataGridExams.DataSource = ds.Tables["Results"];
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var exam = new frmExam();

            if (exam.frmExam_New())
                exam.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridExams.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {
                int.TryParse(dataGridExams.SelectedRows[0].Cells["ExamID"].Value.ToString(), out int examID);

                if (ExamDelete(examID))
                {
                    MessageBox.Show(Properties.Resources.ResourceManager.GetString("deleteSuccess"), Properties.Resources.ResourceManager.GetString("success"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmViewExams_Load(sender, e);
                }
            }
            else
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("oneExamOnly"), Properties.Resources.ResourceManager.GetString("selectedRows"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dataGridExams.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {
                int.TryParse(dataGridExams.SelectedRows[0].Cells["ExamID"].Value.ToString(), out int examID);
                var exam = new frmExam();

                if (exam.frmExam_Open(examID))
                    exam.Show();
            }
            else
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("oneExamOnly"), Properties.Resources.ResourceManager.GetString("selectedRows"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var profile = new frmProfile();
            profile.Show();
        }

        private void frmViewExams_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
