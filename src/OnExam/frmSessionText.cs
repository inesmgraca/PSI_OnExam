using System;
using System.Windows.Forms;

namespace OnExam
{
    public partial class frmSessionText : Form
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

        public ExamAnswer Answer { get; set; }

        public frmSessionText()
        {
            InitializeComponent();
        }

        private void frmSessionText_Load(object sender, EventArgs e)
        {
            lblQuestion.Text = QuestionExam.Question;

            if (Answer != null)
            {
                txtAnswer.Text = Answer.AnswerText;
                txtAnswer.Enabled = false;
            }
        }

        public ExamAnswer Save()
        {
            var examAnswer = new ExamAnswer
            {
                QuestionID = QuestionExam.QuestionID,
                Type = QuestionExam.Type,
                AnswerText = txtAnswer.Text
            };

            return examAnswer;
        }
    }
}
