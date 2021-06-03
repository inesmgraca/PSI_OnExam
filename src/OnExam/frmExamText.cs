using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExamText : Form
    {
        private ExamQuestion examQuestion { get; set; }

        public frmExamText()
        {
            InitializeComponent();
        }

        public void frmExamText_New()
        {
            examQuestion = new ExamQuestion()
            {
                PerguntaID = 0,
                Tipo = QuestionType.Text
            };
        }

        public void frmExamText_Open(ExamQuestion question)
        {
            examQuestion = question;
            txtQuestion.Text = question.Enunciado;
            txtNotesAnswer.Text = question.Notas;
        }

        public ExamQuestion frmExamText_Save()
        {
            examQuestion.Enunciado = txtQuestion.Text;
            examQuestion.Notas = txtNotesAnswer.Text;
            return examQuestion;
        }
    }
}
