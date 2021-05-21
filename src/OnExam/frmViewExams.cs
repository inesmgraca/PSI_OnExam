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
            var ds = ExamsView();
            dataGridExams.DataSource = ds.Tables["Results"];
        }

        private void btnNew_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dataGridExams.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {

            }
            else
            {
                MessageBox.Show("You must select only one row.", "Selected rows!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Exams_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            var profile = new frmProfile();
            profile.Show();
        }
    }
}
