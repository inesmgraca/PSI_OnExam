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
    public partial class Exams : Form
    {
        public Exams()
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

        }

        private void Exams_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!MainForm.mainForm.Visible && Application.OpenForms.Count == 1)
                Application.Exit();
        }
    }
}
