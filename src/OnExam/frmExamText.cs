using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExamText : Form
    {
        private ExamQuestion examQuestion { get; set; }

        public bool isEdit { get; set; }

        public frmExamText()
        {
            InitializeComponent();
        }

        public void frmExamText_New()
        {
            examQuestion = new ExamQuestion()
            {
                QuestionID = 0,
                Type = QuestionType.Text
            };
        }

        public void frmExamText_Open(ExamQuestion question)
        {
            examQuestion = question;
            txtQuestion.Text = question.Question;
            txtNotes.Text = question.Notes;
            btnDeleteQuestion.Enabled = isEdit;
            txtQuestion.Enabled = isEdit;
        }

        public ExamQuestion frmExamText_Save()
        {
            examQuestion.Question = txtQuestion.Text;
            examQuestion.Notes = txtNotes.Text;
            return examQuestion;
        }

        private void btnDeleteQuestion_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (examQuestion.QuestionID == 0)
                    Close();
                else
                {
                    if (ExamDeleteQuestion(examQuestion.QuestionID))
                        Close();
                }
            }
        }
    }
}
