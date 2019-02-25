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
using AttKeeper.man.obj;

namespace AttKeeper.form
{
    public partial class MainForm : Form
    {
        public User User { get; set; }

        private ManageDataForm _manageDataForm;
        private DTRForm _dtrForm;
        private SettingForm _settingForm;

        private Company _company;
        public MainForm()
        {
            InitializeComponent();
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _manageDataForm = new ManageDataForm
            {
                WindowState = FormWindowState.Maximized,
                MdiParent = this
            };
            _manageDataForm.Show();
        }

        private void dTRManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _dtrForm = new DTRForm
            {
                WindowState = FormWindowState.Maximized,
                MdiParent = this,
                User = User
            };
            _dtrForm.Show();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_settingForm = new SettingForm
            //{
            //    MdiParent = this,
            //    WindowState = FormWindowState.Maximized
            //};
            //_settingForm.Show();
            var f = new SettingWizard
            {
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.ShowDialog();
        }

        private void perferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new PreferencesForm
            {
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MinimizeBox = false,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.ShowDialog();
        }

        private void generateDTRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new ImportLogForm
            {
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                User = User
            };
            f.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripStatusLabelUser.Text = String.Format(@"User: {0}", User.UserName.ToUpper());
            GetCompany();
        }

        private void GetCompany()
        {
            _company = CompanyManager.Get();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;
            if (MessageBox.Show(@"Are you sure want close the application?",
                @"Attendance Keeper v1.1",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information) == DialogResult.OK)
                Environment.Exit(1);
            else
                e.Cancel = true; // to don't close form is user change his mind
        }

        private void batchDTRPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var df = new ReportWizardForm { MaximizeBox = false, MinimizeBox = false, StartPosition = FormStartPosition.CenterScreen};
            df.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _manageDataForm = new ManageDataForm
            {
                WindowState = FormWindowState.Maximized,
                MdiParent = this
            };
            _manageDataForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var f = new SettingWizard
            {
                FormBorderStyle = FormBorderStyle.FixedSingle,
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            f.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var f = new ImportLogForm
            {
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                User = User
            };
            f.ShowDialog();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            _dtrForm = new DTRForm
            {
                WindowState = FormWindowState.Maximized,
                MdiParent = this,
                User = User,
                Company = _company
            };
            _dtrForm.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var df = new ReportWizardForm { MaximizeBox = false, MinimizeBox = false, StartPosition = FormStartPosition.CenterScreen };
            df.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new UserForm
            {
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle
            };
            f.ShowDialog();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelTime.Text = String.Format(@"{0} - {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
        }
    }
}
