using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExamText : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private ExamQuestion examQuestion { get; set; }

        public bool isEdit { get; set; }

        public frmExamText()
        {
            InitializeComponent();
        }

        public void New()
        {
            examQuestion = new ExamQuestion()
            {
                QuestionID = 0,
                Type = QuestionType.Text
            };
        }

        public void Open(ExamQuestion question)
        {
            examQuestion = question;
            txtQuestion.Text = question.Question;
            txtNotes.Text = question.Notes;
            btnDeleteQuestion.Enabled = isEdit;
            txtQuestion.Enabled = isEdit;
        }

        public ExamQuestion Save()
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
