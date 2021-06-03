using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExam : Form
    {
        private int ExamID { get; set; }

        public frmExam()
        {
            InitializeComponent();
        }

        public bool frmExam_New()
        {
            var result = false;
            var examID = ExamAdd(10, false);

            if (examID != 0)
            {
                txtExamName.Text = examID.ToString();
                txtDuration.Text = "10";
                chkIsRandom.Checked = false;
                ExamID = examID;
                result = true;
            }

            return result;
        }

        public bool frmExam_Open(int examID)
        {
            var result = false;
            var examDetails = ExamOpen(examID);

            if (examDetails != null)
            {
                txtExamName.Text = examDetails.ExamName;
                txtDuration.Text = examDetails.Duration;
                chkIsRandom.Checked = examDetails.isRandom;
                ExamID = examID;

                var examQuestions = ExamOpenQuestions(ExamID);

                if (examQuestions != null)
                {
                    foreach (var examQuestion in examQuestions)
                    {
                        if (examQuestion.Tipo == QuestionType.Text)
                        {
                            var examText = new frmExamText();
                            examText.MdiParent = this;
                            examText.frmExamText_Open(examQuestion);
                            examText.Show();
                        }
                        else
                        {
                            var examOpts = new frmExamOpts();
                            examOpts.MdiParent = this;
                            examOpts.flowPanelOptions_Open(examQuestion);
                            examOpts.Show();
                        }
                    }
                }

                result = true;
            }

            return result;
        }

        private void stripBtnRdb_Click(object sender, EventArgs e)
        {
            var examOpts = new frmExamOpts();
            examOpts.MdiParent = this;
            examOpts.flowPanelOptions_New(QuestionType.RadioButton);
            examOpts.Show();
        }

        private void stripBtnChk_Click(object sender, EventArgs e)
        {
            var examOpts = new frmExamOpts();
            examOpts.MdiParent = this;
            examOpts.flowPanelOptions_New(QuestionType.Checkbox);
            examOpts.Show();
        }

        private void stripBtnTxt_Click(object sender, EventArgs e)
        {
            var examText = new frmExamText();
            examText.MdiParent = this;
            examText.frmExamText_New();
            examText.Show();
        }

        private void frmExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            var close = true;
            var exam = new ExamDetails()
            {
                ExamName = txtExamName.Text,
                Duration = txtDuration.Text,
                isRandom = chkIsRandom.Checked,
                Perguntas = new List<ExamQuestion>()
            };

            if (MdiChildren.Length > 0)
            {
                for (int i = 0; i < MdiChildren.Length; i++)
                {
                    if (MdiChildren[i] is frmExamText)
                    {
                        var frmQuestion = (frmExamText)MdiChildren[i];
                        var examQuestion = frmQuestion.frmExamText_Save();

                        if (examQuestion.PerguntaID != 0)
                        {
                            if (!ExamUpdateQuestion(examQuestion))
                            {
                                close = false;
                                break;
                            }
                        }
                        else
                            exam.Perguntas.Add(examQuestion);
                    }
                    else
                    {
                        var frmQuestion = (frmExamOpts)MdiChildren[i];
                        var examQuestion = frmQuestion.frmExamOpts_Save();

                        if (examQuestion.PerguntaID != 0)
                        {
                            if (!ExamUpdateQuestion(examQuestion))
                            {
                                close = false;
                                break;
                            }
                        }
                        else
                            exam.Perguntas.Add(examQuestion);
                    }
                }                

                if (!ExamUpdateExam(ExamID, exam))
                    close = false;
            }

            if (close)
                MessageBox.Show(Properties.Resources.ResourceManager.GetString("success"), Properties.Resources.ResourceManager.GetString("saveSuccess"), MessageBoxButtons.OK);
            else
            {
                if (MessageBox.Show(Properties.Resources.ResourceManager.GetString("error"), Properties.Resources.ResourceManager.GetString("saveError"),
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }
    }
}
