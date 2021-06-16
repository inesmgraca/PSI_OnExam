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

        public ExamQuestion QuestionExam { get; set; }

        public bool isEdit { get; set; }

        public frmExamText()
        {
            InitializeComponent();
        }

        private void frmExamText_Load(object sender, System.EventArgs e)
        {
            if (QuestionExam == null)
            {
                QuestionExam = new ExamQuestion()
                {
                    QuestionID = 0,
                    Type = QuestionType.Text
                };
            }
            else
            {
                txtQuestion.Text = QuestionExam.Question;
                txtNotes.Text = QuestionExam.Notes;
                btnDeleteQuestion.Enabled = isEdit;
                txtQuestion.Enabled = isEdit;
            }
        }

        public ExamQuestion Save()
        {
            QuestionExam.Question = txtQuestion.Text;
            QuestionExam.Notes = txtNotes.Text;
            return QuestionExam;
        }

        private void btnDeleteQuestion_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (QuestionExam.QuestionID == 0)
                    Close();
                else
                {
                    if (ExamDeleteQuestion(QuestionExam.QuestionID))
                        Close();
                }
            }
        }
    }
}
