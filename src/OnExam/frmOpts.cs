using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnExam
{
    public partial class frmOpts : Form
    {
        private OptionType optionType { get; set; }

        public enum OptionType
        {
            RadioButtons,
            Checkboxes
        }

        public frmOpts()
        {
            InitializeComponent();
        }

        public void flowPanelOptions_Fill(OptionType optType)
        {
            optionType = optType;

            for (int i = 1; i <= 4; i++)
            {
                Control option;

                if (optType == OptionType.RadioButtons)
                {
                    option = new RadioButton()
                    {
                        Name = $"option{i}",
                        Tag = i,
                        Text = i.ToString(),
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
                        Text = i.ToString(),
                        Height = 50,
                        Width = 625
                    };
                }

                cmbEdit.Items.Add(new ItemData(option.Text));
                flowPanelOptions.Controls.Add(option);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Control option;

            var i = flowPanelOptions.Controls.Count + 1;

            if (optionType == OptionType.RadioButtons)
            {
                option = new RadioButton()
                {
                    Name = $"option{i}",
                    Tag = i,
                    Text = i.ToString(),
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
                    Text = i.ToString(),
                    Height = 50,
                    Width = 625
                };
            }

            cmbEdit.Items.Add(new ItemData(option.Text));
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
