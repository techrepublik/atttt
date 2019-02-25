using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AttKeeper.data;
using AttKeeper.core.sta;

namespace AttKeeper.form
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if ((waterMarkTextBoxUserName.Text.Length > 0) && (waterMarkTextBoxPassword.Text.Length > 0))
            {
                var user = Validation.GetActiveUser(waterMarkTextBoxUserName.Text.ToUpper(),
                    waterMarkTextBoxPassword.Text.ToUpper());
                if (user != null)
                {
                    var f = new MainForm {User = user};
                    f.Show();
                    Hide();
                }
                else
                {
                    labelMessage.Text = @"Invalid User Account. Please verify account and try again.";
                    labelMessage.ForeColor = Color.Red;
                }
            }
            else
            {
                labelMessage.Text = @"Invalid User Account. Please verify account and try again.";
                labelMessage.ForeColor = Color.Red;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
