using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using AttKeeper.core.sta;
using AttKeeper.data;
using UtilityManager.ui;

namespace AttKeeper.form
{
    public partial class LoginForm : Form
    {
        private Configuration _config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
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
                var superCode = ConfigurationManager.AppSettings["KeyCode"];
                var superName = ConfigurationManager.AppSettings["KeyName"];
                var user = Validation.GetActiveUser(waterMarkTextBoxUserName.Text.ToUpper(),
                    waterMarkTextBoxPassword.Text.ToUpper());
                if (user != null)
                {
                    var f = new MainForm {User = user};
                    f.Show();
                    Hide();
                }
                else if ((superCode == waterMarkTextBoxPassword.Text) && (waterMarkTextBoxUserName.Text == superName))
                {
                    var f = new MainForm
                    {
                        User = new User
                        {
                            UserLevel = 3,
                            UserFullName = @"Supervisor",
                            UserName = @"SuperAdmin"
                        }
                    };
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

        private void waterMarkTextBoxUserName_Enter(object sender, EventArgs e)
        {
            UtilityManager.util.UtilClass.ChangeColor(sender as WaterMarkTextBox, true);
        }

        private void waterMarkTextBoxUserName_Leave(object sender, EventArgs e)
        {
            UtilityManager.util.UtilClass.ChangeColor(sender as WaterMarkTextBox, false);
        }

        private void waterMarkTextBoxPassword_Enter(object sender, EventArgs e)
        {
            UtilityManager.util.UtilClass.ChangeColor(sender as WaterMarkTextBox, true);
        }

        private void waterMarkTextBoxPassword_Leave(object sender, EventArgs e)
        {
            UtilityManager.util.UtilClass.ChangeColor(sender as WaterMarkTextBox, false);
        }
    }
}
