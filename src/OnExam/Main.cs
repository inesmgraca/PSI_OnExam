using System;
using System.Windows.Forms;

namespace OnExam
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var login = new Login();
            login.Show();
            Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();
            Hide();
        }

        private void btnTakeExam_Click(object sender, EventArgs e)
        {
            var takeExam = new TakeExam();
            takeExam.Show();
            Hide();
        }
    }

    public class MainForm
    {
        private static Main _mainForm;

        public static Main mainForm
        {
            get
            {
                if (_mainForm == null)
                    _mainForm = new Main();

                return _mainForm;
            }
        }
    }
}
