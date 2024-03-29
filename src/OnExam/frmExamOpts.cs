﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.Properties.Resources;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExamOpts : Form
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

        public QuestionType TypeQuestion { get; set; }

        public ExamQuestion QuestionExam { get; set; }

        public bool isEdit { get; set; }

        public frmExamOpts()
        {
            InitializeComponent();
        }

        private void frmExamOpts_Load(object sender, EventArgs e)
        {
            if (QuestionExam == null)
            {
                QuestionExam = new ExamQuestion()
                {
                    QuestionID = 0,
                    Type = TypeQuestion
                };

                for (int i = 1; i <= 4; i++)
                {
                    Control option;

                    if (QuestionExam.Type == QuestionType.RadioButton)
                    {
                        option = new RadioButton()
                        {
                            Name = $"option{i}",
                            Tag = i,
                            Text = Properties.Resources.ResourceManager.GetString("answer") + i.ToString(),
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
                            Text = Properties.Resources.ResourceManager.GetString("answer") + i.ToString(),
                            Height = 50,
                            Width = 625
                        };
                    }

                    cmbEdit.Items.Add(new ItemData(Properties.Resources.ResourceManager.GetString("option") + i));
                    flowPanelOptions.Controls.Add(option);
                }
            }
            else
            {
                txtQuestion.Text = QuestionExam.Question;
                btnAdd.Enabled = isEdit;
                btnDelete.Enabled = isEdit;
                btnDeleteQuestion.Enabled = isEdit;
                txtQuestion.Enabled = isEdit;
                txtText.Enabled = isEdit;
                var i = 1;

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
                            Width = 625,
                            Checked = questionDetails.isRight,
                            Enabled = isEdit
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
                            Width = 625,
                            Checked = questionDetails.isRight,
                            Enabled = isEdit
                        };
                    }

                    cmbEdit.Items.Add(new ItemData(Properties.Resources.ResourceManager.GetString("option") + i));
                    flowPanelOptions.Controls.Add(option);
                    i++;
                }

                if (flowPanelOptions.Controls.Count > 2)
                    btnDelete.Enabled = true;
                else
                    btnDelete.Enabled = false;

                if (i - 1 < 6)
                    btnAdd.Enabled = true;
                else
                    btnAdd.Enabled = false;

                if (!isEdit)
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        public ExamQuestion Save()
        {
            QuestionExam.Question = txtQuestion.Text;
            QuestionExam.Notes = "";
            QuestionExam.QuestionDetails = new List<ExamQuestionDetails>();

            for (int i = 0; i < flowPanelOptions.Controls.Count; i++)
            {
                if (flowPanelOptions.Controls[i] is RadioButton)
                {
                    var option = (RadioButton)flowPanelOptions.Controls[i];

                    var questionDetails = new ExamQuestionDetails
                    {
                        Text = option.Text,
                        isRight = option.Checked
                    };

                    QuestionExam.QuestionDetails.Add(questionDetails);
                }
                else
                {
                    var option = (CheckBox)flowPanelOptions.Controls[i];

                    var questionDetails = new ExamQuestionDetails
                    {
                        Text = option.Text,
                        isRight = option.Checked
                    };

                    QuestionExam.QuestionDetails.Add(questionDetails);
                }
            }

            return QuestionExam;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Control option;
            var i = flowPanelOptions.Controls.Count + 1;

            if (QuestionExam.Type == QuestionType.RadioButton)
            {
                option = new RadioButton()
                {
                    Name = $"option{i}",
                    Tag = i,
                    Text = Properties.Resources.ResourceManager.GetString("answer") + i,
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
                    Text = Properties.Resources.ResourceManager.GetString("answer") + i,
                    Height = 50,
                    Width = 625
                };
            }

            cmbEdit.Items.Add(new ItemData(Properties.Resources.ResourceManager.GetString("option") + i));
            flowPanelOptions.Controls.Add(option);

            if (i == 6)
                btnAdd.Enabled = false;
            if (i > 2)
                btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var i = flowPanelOptions.Controls.Count;

            foreach (Control c in flowPanelOptions.Controls)
            {
                if (c.Name == $"option{i}")
                    flowPanelOptions.Controls.Remove(c);
            }

            if (i - 1 == 2)
                btnDelete.Enabled = false;
            if (i - 1 < 6)
                btnAdd.Enabled = true;

            cmbEdit.Items.RemoveAt(cmbEdit.Items.Count - 1);
        }

        private void cmbEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control option in flowPanelOptions.Controls)
            {
                if (option.Name == $"option{cmbEdit.SelectedIndex + 1}")
                    txtText.Text = option.Text;
            }
        }

        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            foreach (Control option in flowPanelOptions.Controls)
            {
                if (option.Name == $"option{cmbEdit.SelectedIndex + 1}")
                    option.Text = txtText.Text;
            }
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ResourceManager.GetString("deleteQuestion"), ResourceManager.GetString("areYouSure"), MessageBoxButtons.OKCancel) == DialogResult.OK)
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

    public class ItemData
    {
        public string Text { get; set; }

        public ItemData(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
