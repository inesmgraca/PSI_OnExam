using System;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.SessionManagement;

namespace OnExam
{
    public partial class frmViewSessions : Form
    {
        public int ExamID { get; set; }

        public string ExamName { get; set; }

        public frmViewSessions()
        {
            InitializeComponent();
        }

        private void frmViewSessions_Load(object sender, EventArgs e)
        {
            Text += ExamName;
            var ds = SessionsView(ExamID);
            if (ds != null)
                dataGridAnswers.DataSource = ds.Tables["Sessions"];
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dataGridAnswers.Rows.GetRowCount(DataGridViewElementStates.Selected) == 1)
            {
                SessionManagement.SessionID = (int)dataGridAnswers.SelectedRows[0].Cells["SessionID"].Value;

                var answers = new frmAnswers();

                answers.ExamName = ExamName;
                answers.SessionName = dataGridAnswers.SelectedRows[0].Cells["SessionName"].Value.ToString();
                answers.SessionInfo = dataGridAnswers.SelectedRows[0].Cells["Info"].Value.ToString();

                answers.Show();
            }
            else
                MessageBox.Show(ResourceManager.GetString("oneExamOnly"), ResourceManager.GetString("selectedRows"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmViewSessions_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
