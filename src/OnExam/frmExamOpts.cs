using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static OnExam.ExamManagement;

namespace OnExam
{
    public partial class frmExamOpts : Form
    {
        private ExamQuestion examQuestion { get; set; }

        public frmExamOpts()
        {
            InitializeComponent();
        }

        public void flowPanelOptions_New(QuestionType optType)
        {
            examQuestion = new ExamQuestion()
            {
                PerguntaID = 0,
                Tipo = optType
            };

            for (int i = 1; i <= 4; i++)
            {
                Control option;

                if (optType == QuestionType.RadioButton)
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

        public void flowPanelOptions_Open(ExamQuestion question)
        {
            examQuestion = question;
            txtQuestion.Text = examQuestion.Enunciado;
            var i = 1;

            foreach (var questionDetails in question.DetalhesPergunta)
            {
                Control option;

                if (question.Tipo == QuestionType.RadioButton)
                {
                    option = new RadioButton()
                    {
                        Name = $"option{i}",
                        Tag = i,
                        Text = questionDetails.Espaco1,
                        Height = 50,
                        Width = 625,
                        Checked = questionDetails.isRight
                    };
                }
                else
                {
                    option = new CheckBox()
                    {
                        Name = $"option{i}",
                        Tag = i,
                        Text = questionDetails.Espaco1,
                        Height = 50,
                        Width = 625,
                        Checked = questionDetails.isRight
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

            if (i - 1 < 8)
                btnAdd.Enabled = true;
            else
                btnAdd.Enabled = false;
        }

        public ExamQuestion frmExamOpts_Save()
        {
            examQuestion.Enunciado = txtQuestion.Text;
            examQuestion.Notas = "";
            examQuestion.DetalhesPergunta = new List<ExamQuestionDetails>();

            for (int i = 0; i < flowPanelOptions.Controls.Count; i++)
            {
                if (flowPanelOptions.Controls[i] is RadioButton)
                {
                    var option = (RadioButton)flowPanelOptions.Controls[i];

                    var questionDetails = new ExamQuestionDetails
                    {
                        Espaco1 = option.Text,
                        isRight = option.Checked
                    };

                    examQuestion.DetalhesPergunta.Add(questionDetails);
                }
                else
                {
                    var option = (CheckBox)flowPanelOptions.Controls[i];

                    var questionDetails = new ExamQuestionDetails
                    {
                        Espaco1 = option.Text,
                        isRight = option.Checked
                    };

                    examQuestion.DetalhesPergunta.Add(questionDetails);
                }
            }

            return examQuestion;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Control option;
            var i = flowPanelOptions.Controls.Count + 1;

            if (examQuestion.Tipo == QuestionType.RadioButton)
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
            if (i - 1 < 8)
                btnAdd.Enabled = true;

            cmbEdit.Items.RemoveAt(cmbEdit.Items.Count - 1);
        }

        private void cmbEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control option in flowPanelOptions.Controls)
            {
                if (option.Name == $"option{cmbEdit.SelectedIndex + 1}")
                    txtAnswer.Text = option.Text;
            }
        }

        private void txtAnswer_TextChanged(object sender, EventArgs e)
        {
            foreach (Control option in flowPanelOptions.Controls)
            {
                if (option.Name == $"option{cmbEdit.SelectedIndex + 1}")
                    option.Text = txtAnswer.Text;
            }
        }

        private void btnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (examQuestion.PerguntaID == 0)
                    Close();
                else
                {
                    if (ExamDeleteQuestion(examQuestion.PerguntaID))
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
