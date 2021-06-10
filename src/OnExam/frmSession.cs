using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.ExamManagement;
using static OnExam.SessionManagement;

namespace OnExam
{
    public partial class frmSession : Form
    {
        public frmSession()
        {
            InitializeComponent();
        }

        private void frmTakeExam_Load(object sender, EventArgs e)
        {
            Text += DetailsExam.ExamName;
            Enabled = false;
            var i = 1;

            foreach (var examQuestion in DetailsExam.Questions)
            {
                if (examQuestion.Type == QuestionType.Text)
                {
                    var examText = new frmSessionText();
                    examText.MdiParent = this;
                    examText.Text += i;
                    examText.QuestionExam = examQuestion;
                    examText.Show();
                }
                else
                {
                    var examOpts = new frmSessionOpts();
                    examOpts.MdiParent = this;
                    examOpts.Text += i;
                    examOpts.QuestionExam = examQuestion;
                    examOpts.Show();
                }

                i++;
            }

            if (MessageBox.Show(ResourceManager.GetString("aboutToStart"), ResourceManager.GetString("verifyStart"), MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Enabled = true;
                timerExam.Interval = DetailsExam.Duration * 60000;
                timerExam.Enabled = true;
            }
            else
                Close();
        }

        private void stripBtnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResourceManager.GetString("aboutToClose"), ResourceManager.GetString("areYouSure"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                Close();
        }

        private void timerExam_Tick(object sender, EventArgs e)
        {
            MessageBox.Show(ResourceManager.GetString("examEnded"), ResourceManager.GetString("timeEnded"), MessageBoxButtons.OK);
            Close();
        }

        private void frmTakeExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            var close = true;
            var examAnswers = new List<ExamAnswer>();

            for (int i = 0; i < MdiChildren.Length; i++)
            {
                if (MdiChildren[i] is frmSessionText)
                {
                    var frmQuestion = (frmSessionText)MdiChildren[i];
                    var examAnswer = frmQuestion.Save();

                    if (examAnswer != null)
                        examAnswers.Add(examAnswer);
                    else
                    {
                        close = false;
                        break;
                    }
                }
                else
                {
                    var frmQuestion = (frmSessionOpts)MdiChildren[i];
                    var examAnswer = frmQuestion.Save();

                    if (examAnswer != null)
                        examAnswers.Add(examAnswer);
                    else
                    {
                        close = false;
                        break;
                    }
                }
            }

            if (!close && !SessionClose(examAnswers))
                close = false;

            if (close)
                MessageBox.Show(ResourceManager.GetString("saveSuccess"), ResourceManager.GetString("success"), MessageBoxButtons.OK);
            else
                e.Cancel = true;
        }

        private void frmTakeExam_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
