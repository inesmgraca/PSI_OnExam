using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.ExamManagement;
using static OnExam.SessionManagement;

namespace OnExam
{
    public partial class frmAnswers : Form
    {
        public List<ExamQuestion> Questions { get; set; }

        public List<ExamAnswer> Answers { get; set; }

        public string ExamName { get; set; }

        public string SessionName { get; set; }

        public string SessionInfo { get; set; }

        public frmAnswers()
        {
            InitializeComponent();
        }

        private void frmAnswers_Load(object sender, EventArgs e)
        {
            Text += $"{ExamName} - {SessionName}";
            stripLblName.Text += SessionName;
            stripLblInfo.Text += SessionInfo;
            var i = 1;

            Questions = SessionQuestionsOpen();

            if (Questions != null)
            {
                Answers = SessionAnswersOpen(Questions);

                foreach (var Question in Questions)
                {
                    if (Question.Type == QuestionType.Text)
                    {
                        var sessionText = new frmSessionText();
                        sessionText.MdiParent = this;
                        sessionText.Text += i;
                        sessionText.QuestionExam = Question;

                        foreach (var Answer in Answers)
                        {
                            if (Answer.QuestionID == Question.QuestionID)
                            {
                                sessionText.Answer = Answer;
                                break;
                            }
                        }

                        sessionText.Show();
                    }
                    else
                    {
                        var sessionOpts = new frmSessionOpts();
                        sessionOpts.MdiParent = this;
                        sessionOpts.Text += i;
                        sessionOpts.QuestionExam = Question;

                        foreach (var Answer in Answers)
                        {
                            if (Answer.QuestionID == Question.QuestionID)
                            {
                                sessionOpts.Answer = Answer;
                                break;
                            }
                        }

                        sessionOpts.Show();
                    }

                    i++;
                }
            }
        }
    }
}
