using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AttKeeper.core;
using AttKeeper.core.dtr;
using AttKeeper.core.sta;
using AttKeeper.data;
using AttKeeper.man.obj;
using JBiometric.Entities;

namespace AttKeeper.form
{
    public partial class ReportWizardForm : Form
    {

        List<DtrData> lDTRSource = new List<DtrData>();
        List<EmployeeData> listEnrollee = new List<EmployeeData>();

        private int _iDepartmentId;
        private int iMonth;
        private int iStartDay;
        private int iEndDay;
        private int iYear = DateTime.Now.Year;
        
        public ReportWizardForm()
        {
            InitializeComponent();
        }

        private void ReportWizardForm_Load(object sender, EventArgs e)
        {

            LoadDepartments();
            InitComponents();
        }

        private void InitComponents()
        {
            //labelCompanyName.Text = CompanyName;
            comboBoxMonth.DataSource = UtilityManager.util.UtilClass.FillMonth();

            comboBoxYear.DataSource = UtilityManager.util.UtilClass.FillYear();

            comboBoxStartDay.Items.Clear();
            for (int i = 0; i < 31; i++)
            {
                comboBoxStartDay.Items.Add(i + 1);
            }
            comboBoxStartDay.SelectedIndex = 0;

            comboBoxEndDay.Items.Clear();
            for (int i = 0; i < 31; i++)
            {
                comboBoxEndDay.Items.Add(i + 1);
            }
            comboBoxEndDay.SelectedIndex = 0;

            buttonBack.Enabled = false;
            buttonNext.Enabled = true;
            buttonFinish.Enabled = false;
        }

        private void LoadDepartments()
        {
            Cursor = Cursors.WaitCursor;
            comboBoxDepartment.Items.Clear();
            List<Department> listDepartment = DepartmentManager.GetAll();
            foreach (var department in listDepartment)
            {
                comboBoxDepartment.Items.Add(department);
            }
            comboBoxDepartment.DisplayMember = "DepartmentName";
            comboBoxDepartment.ValueMember = "DepartmentId";
            comboBoxDepartment.Items.Insert(0, "--- All Depatments ---");
            comboBoxDepartment.SelectedIndex = 0;
            Cursor = Cursors.Default;
        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDepartment.SelectedIndex > 0)
            {
                _iDepartmentId = ((Department)comboBoxDepartment.SelectedItem).DepartmentId;
                LoadEnrollees(true);
            }
            else
            {
                LoadEnrollees(false);
            }
        }
        private void LoadEnrollees(bool bDepartment)
        {
            Cursor = Cursors.WaitCursor;
            List<EmployeeData> lEnrollee = bDepartment ? Queries.GetEmployeeData(_iDepartmentId) : Queries.GetEmployeeData();
            jEnrolleeBindingSource1.DataSource = lEnrollee;
            //listBox2.DataSource = listEnrollee;
            //listBox2.DisplayMember = "GetFullName";
            //listBox2.ValueMember = "EnrolleeNo";
            //toolStripStatusLabel1.Text = String.Format("{0} enrollee(s) loaded.", listEnrollee.Count);
            Cursor = Cursors.Default;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            buttonBack.Enabled = false;
            buttonNext.Enabled = true;
            buttonFinish.Enabled = false;
            groupBoxEnrollees.Visible = false;
            //groupBoxDuration.Visible = true;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            buttonBack.Enabled = true;
            buttonNext.Enabled = false;
            buttonFinish.Enabled = true;
            groupBoxEnrollees.Visible = true;
            labelSetDuration.Text = comboBoxMonth.Text + @" " + comboBoxStartDay.Text + @" - " + comboBoxEndDay.Text + @", " + comboBoxYear.Text;
            //groupBoxDuration.Visible = false;
        }

        private void buttonFinish_Click(object sender, EventArgs e)
        {
            //Miscellaneou misc = ActionClass.FillMiscellaneous().FirstOrDefault(mi => mi.MiscActive == true);
            foreach (var item in listEnrollee)
            {
                LoadDRTEnrollee(item);
            }
            MessageBox.Show(lDTRSource.Count.ToString() + @" Record(s) found. Press Ok to continue.", @"Records", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (lDTRSource.Count > 0)
            {
                var f = new ReportWizardDetailFormcs();
                f.LoadData(lDTRSource);
                f.Show();
            }
        }

        private void buttonGetAll_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count <= 0) return;
            var tempEnrollee = jEnrolleeBindingSource1.CurrencyManager.List.Cast<EmployeeData>().ToList();
            if (tempEnrollee.Count == 0) return;
            foreach (var item in tempEnrollee)
            {
                listBox1.Items.Add(item.GetFullName);
            }
            listEnrollee.AddRange(tempEnrollee);
            jEnrolleeBindingSource1.CurrencyManager.List.Clear();
            jEnrolleeBindingSource1.DataSource = null;
        }

        private void buttonGetOne_Click(object sender, EventArgs e)
        {
            //listBox1.Items.Add(listBox2.SelectedItem);
            if (listBox2.Items.Count <= 0) return;
            listBox1.Items.Add(listBox2.Text);
            var en = (EmployeeData)jEnrolleeBindingSource1.Current;
            listEnrollee.Add(en);
            jEnrolleeBindingSource1.Remove(en);
        }

        private void buttonRemoveOne_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;
            if (listBox1.Text.Trim().Length == 0) return;
            var enrolee = listEnrollee.FirstOrDefault(en => en.GetFullName == listBox1.Text);
            jEnrolleeBindingSource1.CurrencyManager.List.Add(enrolee);
            listEnrollee.Remove(enrolee);
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void buttonRemoveAll_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;
            if (listEnrollee.Count == 0) return;
            foreach (var item in listEnrollee)
            {
                jEnrolleeBindingSource1.CurrencyManager.List.Add(item);
            }
            listEnrollee.Clear();
            listBox1.Items.Clear();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void LoadDRTEnrollee(EmployeeData en)
        {
            Cursor = Cursors.WaitCursor;
            if (listBox1.Items.Count > 0)
            {
                var eName = en.GetFullName;
                Empoyee enrollee = EmployeeManager.Get(en.EnrolleeId);

                List<DtrData> lDTRSource01 = DTRManagement.LoadDTRViaDTRBatch(enrollee, iMonth, iStartDay, iEndDay, iYear, DTRManager.GetAll(en.EnrolleeId), eName, labelDuration.Text);
                lDTRSource.AddRange(lDTRSource01 as IEnumerable<DtrData>);
            }
            Cursor = Cursors.Default;
        }
        private void GetDuration()
        {
            labelDuration.Text = String.Format(@"{0} {1} - {2}, {3}", comboBoxMonth.Text, comboBoxStartDay.Text, comboBoxEndDay.Text, comboBoxYear.Text);

            iMonth = Convert.ToInt32(comboBoxMonth.SelectedIndex);
            if (comboBoxStartDay.Text.Length > 0)
                iStartDay = Convert.ToInt32(comboBoxStartDay.Text);

            if (comboBoxEndDay.Text.Length > 0)
                iEndDay = Convert.ToInt32(comboBoxEndDay.Text);

            if (comboBoxYear.Text.Length > 0)
                iYear = Convert.ToInt32(comboBoxYear.Text);
        }
        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDuration();
        }

        private void comboBoxStartDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDuration();
        }

        private void comboBoxEndDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDuration();
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDuration();
        }
    }
}
