using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.UserManagement;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExam : Form
    {
        private int ExamID { get; set; }

        private State State { get; set; }

        public frmExam()
        {
            InitializeComponent();
        }

        private void frmExam_Load(object sender, EventArgs e)
        {
            Text += $"{UserLoggedIn}-{ExamID}";
        }

        public bool frmExam_New()
        {
            var examID = ExamAdd(10, false, State.Inactive);

            if (examID != 0)
            {
                stripTxtExamName.Text = examID.ToString();
                nudDuration.Value = 10;
                chkIsRandom.Checked = false;
                ExamID = examID;
                return true;
            }

            return false;
        }

        public bool frmExam_Open(int examID)
        {
            var examDetails = ExamOpen(examID);

            if (examDetails != null)
            {
                stripTxtExamName.Text = examDetails.ExamName;
                nudDuration.Value = examDetails.Duration;
                chkIsRandom.Checked = examDetails.isRandom;
                ExamID = examID;

                State = examDetails.State;

                if (State != State.Inactive)
                {
                    stripBtnChk.Enabled = false;
                    stripBtnRdb.Enabled = false;
                    stripBtnTxt.Enabled = false;
                    stripTxtExamName.Enabled = false;
                    nudDuration.Enabled = false;
                    chkIsRandom.Enabled = false;

                    if (State == State.Active)
                        stripBtnActivate.Text = ResourceManager.GetString("btnCloseExam");
                    else
                        stripBtnActivate.Text = ResourceManager.GetString("btnViewResults");
                }

                if (examDetails.Questions != null)
                {
                    foreach (var examQuestion in examDetails.Questions)
                    {
                        if (examQuestion.Type == QuestionType.Text)
                        {
                            var examText = new frmExamText();
                            examText.MdiParent = this;

                            if (State != State.Inactive)
                                examText.isEdit = false;
                            else
                                examText.isEdit = true;

                            examText.Open(examQuestion);
                            examText.Show();
                        }
                        else
                        {
                            var examOpts = new frmExamOpts();
                            examOpts.MdiParent = this;

                            if (State != State.Inactive)
                                examOpts.isEdit = false;
                            else
                                examOpts.isEdit = true;

                            examOpts.Open(examQuestion);
                            examOpts.Show();
                        }
                    }
                }

                return true;
            }
            else
                return false;
        }

        private void stripBtnRdb_Click(object sender, EventArgs e)
        {
            var examOpts = new frmExamOpts();
            examOpts.MdiParent = this;
            examOpts.New(QuestionType.RadioButton);
            examOpts.Show();
        }

        private void stripBtnChk_Click(object sender, EventArgs e)
        {
            var examOpts = new frmExamOpts();
            examOpts.MdiParent = this;
            examOpts.New(QuestionType.Checkbox);
            examOpts.Show();
        }

        private void stripBtnTxt_Click(object sender, EventArgs e)
        {
            var examText = new frmExamText();
            examText.MdiParent = this;
            examText.New();
            examText.Show();
        }

        private void stripBtnActivate_Click(object sender, EventArgs e)
        {
            if (State == State.Inactive)
            {
                if (MessageBox.Show(ResourceManager.GetString("activateExam"), ResourceManager.GetString("areYouSure"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (ExamUpdateState(ExamID, State.Active))
                        Close();
                }
            }
            else if (State == State.Active)
            {
                if (MessageBox.Show(ResourceManager.GetString("closeExam"), ResourceManager.GetString("areYouSure"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (ExamUpdateState(ExamID, State.Closed))
                    {
                        State = State.Closed;
                        stripBtnActivate.Text = ResourceManager.GetString("btnViewResults");
                    }
                }
            }
            else
            {
                // ver results
            }
        }

        private void frmExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (State == State.Inactive && Convert.ToInt32(nudDuration.Value) != 0)
            {
                var close = true;
                var exam = new ExamDetails()
                {
                    ExamName = stripTxtExamName.Text.Trim(),
                    Duration = Convert.ToInt32(nudDuration.Value),
                    isRandom = chkIsRandom.Checked,
                    Questions = new List<ExamQuestion>()
                };

                if (MdiChildren.Length > 0)
                {
                    for (int i = 0; i < MdiChildren.Length; i++)
                    {
                        if (MdiChildren[i] is frmExamText)
                        {
                            var frmQuestion = (frmExamText)MdiChildren[i];
                            var examQuestion = frmQuestion.Save();

                            if (examQuestion.QuestionID != 0)
                            {
                                if (!ExamUpdateQuestion(examQuestion))
                                {
                                    close = false;
                                    break;
                                }
                            }
                            else
                                exam.Questions.Add(examQuestion);
                        }
                        else
                        {
                            var frmQuestion = (frmExamOpts)MdiChildren[i];
                            var examQuestion = frmQuestion.Save();

                            if (examQuestion.QuestionID != 0)
                            {
                                if (!ExamUpdateQuestion(examQuestion))
                                {
                                    close = false;
                                    break;
                                }
                            }
                            else
                                exam.Questions.Add(examQuestion);
                        }
                    }
                }

                if (!close && !ExamUpdate(ExamID, exam))
                    close = false;

                if (close)
                    MessageBox.Show(ResourceManager.GetString("saveSuccess"), ResourceManager.GetString("success"), MessageBoxButtons.OK);
                else
                {
                    if (MessageBox.Show(ResourceManager.GetString("saveError"), ResourceManager.GetString("error"),
                        MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        e.Cancel = true;
                }
            }
            else if (Convert.ToInt32(nudDuration.Value) == 0)
            {
                MessageBox.Show(ResourceManager.GetString("invalidValue"), ResourceManager.GetString("error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void frmExam_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is frmMain && !Application.OpenForms[0].Visible)
                Application.Exit();
        }
    }
}
