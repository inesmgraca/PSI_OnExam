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
    public partial class frmExam : Form
    {
        public frmExam()
        {
            InitializeComponent();
        }

        private void stripBtnRdb_Click(object sender, EventArgs e)
        {
            var opts = new frmOpts();
            opts.MdiParent = this;
            opts.flowPanelOptions_Fill(frmOpts.OptionType.RadioButtons);
            opts.Show();
        }

        private void stripBtnChk_Click(object sender, EventArgs e)
        {
            var opts = new frmOpts();
            opts.MdiParent = this;
            opts.flowPanelOptions_Fill(frmOpts.OptionType.Checkboxes);
            opts.Show();
        }
    }
}
