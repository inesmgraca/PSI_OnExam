using System;
using System.Windows.Forms;

namespace OnExam
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new frmLogin();
            login.Show();
            Hide();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var signup = new frmSignUp();
            signup.Show();
            Hide();
        }

        private void btnTakeExamInfo_Click(object sender, EventArgs e)
        {
            var takeExam = new frmTakeExam();
            takeExam.Show();
            Hide();
        }
    }
}
