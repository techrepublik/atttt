using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using AttKeeper.core;
using AttKeeper.data;
using AttKeeper.man.obj;
using AttKeeper.core.dtr;
using Microsoft.Reporting.WinForms;

namespace AttKeeper.form
{
    public partial class DTRForm : Form
    {
        public User User { get; set; }
        public Company Company { get; set; }
        public static string STime { get; set; }

        List<DTR> _listDTR = new List<DTR>();
        List<EmployeeData> _listEmpoyee = new List<EmployeeData>();

        private int _iMonth = 0;
        private int _iYear = 0;
        private int _iEnrolleeId = 0;
        private string _setting;
        private string _principal;
        private string _employeeName;
        private string _employeePosition;
        public DTRForm()
        {
            InitializeComponent();
        }

        private void DTRForm_Load(object sender, EventArgs e)
        {
            InitComboBoxes();
            InitData();
            this.reportViewer1.RefreshReport();
        }
        private void InitComboBoxes()
        {
            toolStripComboBoxMOnth.Items.Clear();
            foreach (var month in UtilityManager.util.UtilClass.FillMonth())
            {
                toolStripComboBoxMOnth.Items.Add(month);
            }
            toolStripComboBoxMOnth.SelectedIndex = DateTime.Now.Month;

            toolStripComboBoxYear.Items.Clear();
            foreach (var year in UtilityManager.util.UtilClass.FillYear())
            {
                toolStripComboBoxYear.Items.Add(year);
            }
            toolStripComboBoxYear.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void InitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            _listEmpoyee = core.sta.Queries.GetEmployeeData();
            employeeDataBindingSource.DataSource = _listEmpoyee;
            Cursor.Current = Cursors.Default;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            LoadDRTEnrolleeId();
            if (tabControl1.SelectedTab == tabPage2) toolStripButtonPrint.PerformClick();
        }
        private void LoadDRTEnrolleeId()
        {
            Cursor = Cursors.WaitCursor;
            //try catch here
            if (listBox1.Items.Count > 0)
            {
                _iEnrolleeId = ((EmployeeData)listBox1.SelectedItem).EnrolleeId;
                var enrollee = EmployeeManager.Get(_iEnrolleeId);
                var position = PositionManager.Get(enrollee.PositionId);
                
                if (position != null)
                {
                    _principal = position.PositionName?.Substring(0, 3).ToUpper() ?? @"NO";
                    _employeePosition = position.PositionName;
                }

                _employeeName = String.Format(@"{0} {1}. {2}", enrollee.EmployeeFirstName,
                    enrollee.EmployeeMiddleName.Substring(0, 1), enrollee.EmployeeLastName);
                labelEmployee.Text = String.Format(@"{0} - ({1})", _employeeName, _employeePosition);
                
                _listDTR = DTRManagement.LoadDTRViaDTR(enrollee, _iMonth, _iYear, DTRManager.GetAll(_iEnrolleeId), out _setting);
                dTRBindingSource.DataSource = _listDTR;

                ExecuteSaveDTRThread(); // loop to save dtr.

                MarkUnderOverTimeHours();
            }
            Cursor = Cursors.Default;
        }
        private void MarkUnderOverTimeHours()
        {
            foreach (DataGridViewRow dRow in dTRDataGridView.Rows)
            {
                if (dRow.Cells[10].Value == null) continue;
                var dValue = ((DTR)dRow.DataBoundItem).TotalHours;
                dRow.DefaultCellStyle.ForeColor = (dValue < 0) ? Color.Red : Color.Black;
            }

        }

        private void toolStripComboBoxMOnth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxMOnth.Items.Count > 0)
            {
                _iMonth = toolStripComboBoxMOnth.SelectedIndex;
                if ((_iYear > 0) && (_iEnrolleeId > 0)) listBox1_DoubleClick(sender, e); // LoadDRTEnrolleeId(); 
            }
        }

        private void toolStripComboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBoxYear.Items.Count > 0)
            {
                _iYear = int.Parse(toolStripComboBoxYear.Text);
                if (_iMonth > 0 && (_iEnrolleeId > 0)) listBox1_DoubleClick(sender, e); // LoadDRTEnrolleeId();
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (waterMarkTextBox1.Text.Length <= 0) return;
            employeeDataBindingSource.DataSource =
                _listEmpoyee.FindAll(f => f.LastName.ToUpper().Contains(waterMarkTextBox1.Text.ToUpper()));
            listBox1.DataSource = employeeDataBindingSource;
            Cursor.Current = Cursors.Default;
        }

        private void waterMarkTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonGo.PerformClick();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoadDRTEnrolleeId();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
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

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            var supervisor = ConfigurationManager.AppSettings["Supervisor"];
            var superintendent = ConfigurationManager.AppSettings["Superintendent"];
            var iYear = Convert.ToInt16(toolStripComboBoxYear.Text);
            var iMonth = toolStripComboBoxMOnth.SelectedIndex;
            string SDuration = String.Format(@"{0} {1}-{2}, {3}", toolStripComboBoxMOnth.Text.Trim(), 1, DateTime.DaysInMonth(iYear, iMonth), iYear);
            ReportParameter p1 = new ReportParameter("EnrolleeName", _employeeName);
            ReportParameter p2 = new ReportParameter("DurationLabel", SDuration);
            ReportParameter p3;
            if (System.Configuration.ConfigurationManager.AppSettings["Print-All"] == "True")
            {
                p3 = new ReportParameter("SettingLabel", _setting);
            }
            else
            {
                p3 = new ReportParameter("SettingLabel", @"____________________________________");
            }

            ReportParameter p4;
            switch (_principal)
            {
                case @"PRI":
                    p4 = new ReportParameter("Supervisor", supervisor);
                    break;
                case @"SUP":
                    p4 = new ReportParameter("Supervisor", superintendent);
                    break;
                default:
                    p4 = new ReportParameter("Supervisor", Company.CompanyPrincipal);
                    break;
            }
             
            this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3, p4 });
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            //if (System.Configuration.ConfigurationManager.AppSettings["PrintAll"] == "True")
            dTRBindingSource.DataSource = _listDTR;
            CompanyBindingSource.DataSource = CompanyManager.Get();
            //else
            //{
            //    var tempDTRSource = new List<DTR>();
            //    //for (int i = 0; i < 2; i++)
            //    //{
            //    foreach (var dtr in _listDTR)
            //    {
            //        DTR d = dtr;
            //        d.TotalHour = null;
            //        d.TotalMinute = null;
            //        tempDTRSource.Add(d);
            //    }
            //    //}
            //    dTRBindingSource.DataSource = tempDTRSource;
            //}
            this.reportViewer1.RefreshReport();
        }
        private void GetToUpdateValue(int iColl)
        {
            var df = new DTRUpdateForm
            {
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedSingle,
                StartPosition = FormStartPosition.CenterScreen
            };
            
            DialogResult dResult = DialogResult.None;
            df.IEnrolleeId = ((EmployeeData)listBox1.SelectedItem).EnrolleeId;
            switch (iColl)
            {
                case 2:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                        if (dTRBindingSource != null) ((DTR) dTRBindingSource.Current).TimeInAM = STime;
                    }
                    break;
                case 3:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                    }
                    break;
                case 4:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                    }
                    break;
                case 5:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                    }
                    break;
                case 6:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                    }
                    break;
                case 7:
                    df.SDate = ((DTR)dTRBindingSource?.Current).DTRDate.Value.ToString("yyyy/MM/dd");
                    df.IEnrolleeNo = Convert.ToInt32(((EmployeeData)listBox1.SelectedItem).EnrolleeNo);
                    dResult = df.ShowDialog();
                    if (DialogResult.OK == dResult)
                    {
                        if (dTRDataGridView.CurrentRow != null) dTRDataGridView.CurrentRow.Cells[iColl].Value = STime;
                    }
                    break;
                default:
                    break;
            }
        }
        private void dTRDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetToUpdateValue(e.ColumnIndex);
        }

        private void dTRDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dTRBindingSource?.Current == null) return;
            if (dTRDataGridView.Rows.Count > 0)
            {
                if (dTRDataGridView.IsCurrentRowDirty)
                {
                    Validate();
                    ((DTR)dTRBindingSource.Current).IsSource = true;
                    ((DTR)dTRBindingSource.Current).EditedBy = User.UserName;
                    ((DTR)dTRBindingSource.Current).EditedOn = DateTime.Now;
                    ((DTR)dTRBindingSource.Current).EmployeeId = ((EmployeeData)listBox1.SelectedItem).EnrolleeId;
                    ((DTR)dTRBindingSource.Current).EmployeeNo = ((EmployeeData)listBox1.SelectedItem).EnrolleeNo;
                    dTRBindingSource.EndEdit();
                    var iResult = DTRManager.Save((DTR) dTRBindingSource.Current);
                    Console.WriteLine(iResult > 0 ? @"Row Saved." : @"Row Save Error.");
                }
            }
        }

        private void SaveDTR()
        {
            foreach (var dtr in _listDTR)
            {
                if (dtr.DTRId > 0)
                {
                    int iResult = DTRManager.Save(dtr);
                    if (iResult > 0)
                    {
                        Console.WriteLine(dtr.DTRId.ToString() + " " + dtr.DTRDay);
                    }
                }
            }
        }
        private void ExecuteSaveDTRThread()
        {
            Thread t = new Thread(new ThreadStart(SaveDTR));
            t.IsBackground = true;
            t.Start();
        }

        private void toolStripButtonBatch_Click(object sender, EventArgs e)
        {
            var df = new ReportWizardForm { MaximizeBox = false, MinimizeBox = false, StartPosition = FormStartPosition.CenterScreen };
            df.ShowDialog();
        }

        private void toolStripComboBoxMOnth_Click(object sender, EventArgs e)
        {

        }
    }
}
