using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmSessionOpts : Form
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

        public ExamAnswer Answer { get; internal set; }

        public frmSessionOpts()
        {
            InitializeComponent();
        }

        private void frmSessionOpts_Load(object sender, EventArgs e)
        {
            lblQuestion.Text = QuestionExam.Question;
            var i = 1;

            if (Answer == null)
            {
                foreach (var questionDetails in QuestionExam.QuestionDetails)
                {
                    Control option;

                    if (QuestionExam.Type == QuestionType.RadioButton)
                    {
                        option = new RadioButton()
                        {
                            Name = $"option{i}",
                            Tag = i,
                            Text = questionDetails.Text,
                            Height = 50,
                            Width = 625
                        };
                    }
                    else
                    {
                        option = new CheckBox()
                        {
                            Name = $"option{i}",
                            Tag = i,
                            Text = questionDetails.Text,
                            Height = 50,
                            Width = 625
                        };
                    }

                    flowPanelOptions.Controls.Add(option);
                    i++;
                }
            }
            else
            {
                foreach (var questionDetails in QuestionExam.QuestionDetails)
                {
                    Control option;
                    var selected = false;

                    if (QuestionExam.Type == QuestionType.RadioButton)
                    {
                        if (Answer.AnswerRdb == questionDetails.QuestionDetailsID)
                            selected = true;

                        option = new RadioButton()
                        {
                            Name = $"option{i}",
                            Tag = i,
                            Text = questionDetails.Text,
                            Height = 50,
                            Width = 625,
                            Checked = selected,
                            Enabled = false
                        };
                    }
                    else
                    {
                        foreach (var answer in Answer.AnswerChk)
                        {
                            if (answer == questionDetails.QuestionDetailsID)
                                selected = true;
                        }

                        option = new CheckBox()
                        {
                            Name = $"option{i}",
                            Tag = i,
                            Text = questionDetails.Text,
                            Height = 50,
                            Width = 625,
                            Checked = selected,
                            Enabled = false
                        };
                    }

                    if (questionDetails.isRight)
                    {
                        option.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
                        option.ForeColor = System.Drawing.Color.LimeGreen;
                    }

                    flowPanelOptions.Controls.Add(option);
                    i++;
                }
            }
        }

        public ExamAnswer Save()
        {
            var examAnswer = new ExamAnswer
            {
                QuestionID = QuestionExam.QuestionID,
                Type = QuestionExam.Type
            };

            if (examAnswer.Type == QuestionType.Checkbox)
                examAnswer.AnswerChk = new List<int>();

            for (int i = 0; i < flowPanelOptions.Controls.Count; i++)
            {
                if (flowPanelOptions.Controls[i] is RadioButton)
                {
                    var option = (RadioButton)flowPanelOptions.Controls[i];

                    if (option.Checked)
                    {
                        examAnswer.AnswerRdb = QuestionExam.QuestionDetails[i].QuestionDetailsID;
                        break;
                    }
                }
                else
                {
                    var option = (CheckBox)flowPanelOptions.Controls[i];

                    if (option.Checked)
                        examAnswer.AnswerChk.Add(QuestionExam.QuestionDetails[i].QuestionDetailsID);
                }
            }

            return examAnswer;
        }
    }
}
