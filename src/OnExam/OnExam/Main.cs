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
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var register = new Register();
            register.Show();

        }

        private void btnTakeExam_Click(object sender, EventArgs e)
        {
            var takeExam = new TakeExam();
            takeExam.Show();

        }
    }
}
