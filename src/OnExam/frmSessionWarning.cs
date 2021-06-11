using System;
using System.Windows.Forms;
using static OnExam.Properties.Resources;

namespace OnExam
{
    public partial class frmSessionWarning : Form
    {
        public frmSessionWarning()
        {
            InitializeComponent();
        }

        private void frmSessionWarning_Load(object sender, EventArgs e)
        {
            btnOK.Text = MessageBoxButtons.OK.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MdiParent.Enabled = true;
        }
    }
}
