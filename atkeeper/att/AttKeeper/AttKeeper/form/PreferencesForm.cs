using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using AttKeeper.data;
using AttKeeper.man.obj;
using UtilityManager.util;

namespace AttKeeper.form
{
    public partial class PreferencesForm : Form
    {
        private Configuration _config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            GetCompany();
            LoadInitDepartment();
            LoadInitHoliday();
            LoadInitLeaveType();

            InitConfig();

            groupBox2.Enabled = false;
        }

        private void departmentDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (departmentBindingSource == null) return;
            if (departmentDataGridView.Rows.Count <= 1) return;
            if (!departmentDataGridView.IsCurrentRowDirty) return;
            Validate();
            departmentBindingSource.EndEdit();
            var iResult = DepartmentManager.Save((Department)departmentBindingSource.Current);
            if (iResult > 0) LoadInitDepartment();
        }

        private void LoadInitDepartment()
        {
            Cursor.Current = Cursors.WaitCursor;
            departmentBindingSource.DataSource = DepartmentManager.GetAll();
            Cursor.Current = Cursors.Default;
        }

        private void LoadInitPosition(int iDepartmentId)
        {
            Cursor.Current = Cursors.WaitCursor;
            positionBindingSource.DataSource = PositionManager.GetAll(iDepartmentId);
            Cursor.Current = Cursors.Default;
        }
        private void LoadInitHoliday()
        {
            Cursor.Current = Cursors.WaitCursor;
            holidayBindingSource.DataSource = HolidayManager.GetAll();
            Cursor.Current = Cursors.Default;
        }
        private void LoadInitLeaveType()
        {
            Cursor.Current = Cursors.WaitCursor;
            leaveTypeBindingSource.DataSource = LeaveTypeManager.GetAll();
            Cursor.Current = Cursors.Default;
        }
        private void departmentBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (departmentBindingSource?.Current == null) return;
            LoadInitPosition(((Department)departmentBindingSource.Current).DepartmentId);
        }

        private void positionDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (positionBindingSource == null) return;
            if (positionDataGridView.Rows.Count <= 1) return;
            if (!positionDataGridView.IsCurrentRowDirty) return;
            Validate();
            ((Position) positionBindingSource.Current).DepartmentId =
                ((Department) departmentBindingSource.Current).DepartmentId;
            positionBindingSource.EndEdit();
            var iResult = PositionManager.Save((Position)positionBindingSource.Current);
            if (iResult > 0) LoadInitPosition(((Department)departmentBindingSource.Current).DepartmentId);
        }

        private void holidayDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (holidayBindingSource == null) return;
            if (holidayDataGridView.Rows.Count <= 1) return;
            if (!holidayDataGridView.IsCurrentRowDirty) return;
            Validate();
            holidayBindingSource.EndEdit();
            var iResult = HolidayManager.Save((Holiday)holidayBindingSource.Current);
            if (iResult > 0) LoadInitHoliday();
        }

        private void leaveTypeDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (leaveTypeBindingSource == null) return;
            if (leaveTypeDataGridView.Rows.Count <= 1) return;
            if (!leaveTypeDataGridView.IsCurrentRowDirty) return;
            Validate();
            leaveTypeBindingSource.EndEdit();
            var iResult = LeaveTypeManager.Save((LeaveType)leaveTypeBindingSource.Current);
            if (iResult > 0) LoadInitLeaveType();
        }

        private void buttonCompanySave_Click(object sender, EventArgs e)
        {
            Validate();
            ((Company) companyBindingSource.Current).CompanyId = CheckCompanyId();
            if (companyLogo1PictureBox.Image != null)
                ((Company)companyBindingSource.Current).CompanyLogo1 = ImageClass.ImageToByte(companyLogo1PictureBox.Image);
            if (companyLogo2PictureBox.Image != null)
                ((Company)companyBindingSource.Current).CompanyLogo2 = ImageClass.ImageToByte(companyLogo2PictureBox.Image);
            companyBindingSource.EndEdit();
            var iResult = CompanyManager.Save((Company) companyBindingSource.Current);
            if (iResult > 0)
                MessageBox.Show(@"Company Information saved successfully", @"Save", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(@"Error in saving company information", @"Save", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            companyNameTextBox.Focus();
        }

        private static int CheckCompanyId()
        {
            var listCompnay = CompanyManager.GetAll();
            return listCompnay == null ? 0 : listCompnay.First().CompanyId;
        }

        private void departmentDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteDepartment();
            }
        }

        private void DeleteDepartment()
        {
            if (departmentBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (DepartmentManager.Delete(((Department)departmentBindingSource.Current).DepartmentId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                departmentBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                departmentDataGridView.Focus();
            }
        }

        private void positionDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePosition();
            }
        }
        private void DeletePosition()
        {
            if (positionBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (PositionManager.Delete(((Position)positionBindingSource.Current).PositionId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                positionBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                positionDataGridView.Focus();
            }
        }

        private void holidayDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteHoliday();
            }
        }
        private void DeleteHoliday()
        {
            if (holidayBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (HolidayManager.Delete(((Holiday)holidayBindingSource.Current).HolidayId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                holidayBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                holidayDataGridView.Focus();
            }
        }

        private void leaveTypeDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteLeaveType();
            }
        }
        private void DeleteLeaveType()
        {
            if (leaveTypeBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (LeaveTypeManager.Delete(((LeaveType)leaveTypeBindingSource.Current).LeaveTypeId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                leaveTypeBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                leaveTypeDataGridView.Focus();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = @"Jpeg (*.jpg)|*.jpg|Jpeg (*.jpeg)|*.jpeg|Png (*.png) |*.png|Gif (*.gif) | *.gif";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                companyLogo1PictureBox.ImageLocation = openDialog.FileName;
                byte[] tempBuffer = ImageClass.ReadFile(openDialog.FileName);
                companyLogo1PictureBox.Image = ImageClass.ByteToImage(tempBuffer);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = @"Jpeg (*.jpg)|*.jpg|Jpeg (*.jpeg)|*.jpeg|Png (*.png) |*.png|Gif (*.gif) | *.gif";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                companyLogo2PictureBox.ImageLocation = openDialog.FileName;
                byte[] tempBuffer = ImageClass.ReadFile(openDialog.FileName);
                companyLogo2PictureBox.Image = ImageClass.ByteToImage(tempBuffer);
            }
        }

        private void GetCompany()
        {
            companyBindingSource.DataSource = CompanyManager.Get();
        }

        private void companyBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (companyBindingSource == null) return;
            if (((Company)companyBindingSource.Current).CompanyLogo1 != null)
            {
                companyLogo1PictureBox.Image = ImageClass.ByteToImage(((Company)companyBindingSource.Current).CompanyLogo1.ToArray());
            }
            if (((Company)companyBindingSource.Current).CompanyLogo2 != null)
            {
                companyLogo2PictureBox.Image = ImageClass.ByteToImage(((Company)companyBindingSource.Current).CompanyLogo2.ToArray());
            }
        }

        private void waterMarkTextBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["GraceIn-AM"].Value = waterMarkTextBoxGraceInAM.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update GraceIN-AM due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBox1_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["GraceIn-AM"].Value = waterMarkTextBoxGraceInAM.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void waterMarkTextBoxGraceInPM_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["GraceIn-PM"].Value = waterMarkTextBoxGraceInPM.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void waterMarkTextBoxGraceInOT_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["GraceIn-OT"].Value = waterMarkTextBoxGraceInOT.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void waterMarkTextBoxRegular_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["RegularLabel"].Value = waterMarkTextBoxRegular.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        private void waterMarkTextBoxOvertime_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["OvertimeLabel"].Value = waterMarkTextBoxOvertime.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
            
        }

        private void waterMarkTextBoxUndertime_Leave(object sender, EventArgs e)
        {
            //_config.AppSettings.Settings["UndertimeLabel"].Value = waterMarkTextBoxUndertime.Text;
            //_config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");
        }

        //get data from appconfig file
        private void InitConfig()
        {
            waterMarkTextBoxGraceInAM.Text = ConfigurationManager.AppSettings["GraceIn-AM"];
            waterMarkTextBoxGraceInPM.Text = ConfigurationManager.AppSettings["GraceIn-PM"];
            waterMarkTextBoxGraceInOT.Text = ConfigurationManager.AppSettings["GraceIn-OT"];
            waterMarkTextBoxRegular.Text = ConfigurationManager.AppSettings["RegularLabel"];
            waterMarkTextBoxOvertime.Text = ConfigurationManager.AppSettings["OvertimeLabel"];
            waterMarkTextBoxUndertime.Text = ConfigurationManager.AppSettings["UndertimeLabel"];
            checkBoxShowHourMinDTRPrint.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Print-All"]);
            waterMarkTextBoxSupervisor.Text = ConfigurationManager.AppSettings["Supervisor"];
            waterMarkTextBoxSuper.Text = ConfigurationManager.AppSettings["Superintendent"];
        }

        private void waterMarkTextBoxGraceInAM_Validated(object sender, EventArgs e)
        {
            
            
        }

        private void waterMarkTextBoxGraceInPM_Validated(object sender, EventArgs e)
        {
            

            
        }

        private void waterMarkTextBoxGraceInOT_Validated(object sender, EventArgs e)
        {
            
            
        }

        private void waterMarkTextBoxRegular_Validated(object sender, EventArgs e)
        {
            

            
        }

        private void waterMarkTextBoxOvertime_Validated(object sender, EventArgs e)
        {
            
        }

        private void waterMarkTextBoxUndertime_Validated(object sender, EventArgs e)
        {
            
        }

        private void PreferencesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Validate();
        }

        private void checkBoxShowHourMinDTRPrint_Validated(object sender, EventArgs e)
        {
            
        }

        private void waterMarkTextBoxSupervisor_Validated(object sender, EventArgs e)
        {
            
        }

        private void waterMarkTextBoxSupervisor_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["Supervisor"].Value = waterMarkTextBoxSupervisor.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update Supervisor Setting due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void checkBoxShowHourMinDTRPrint_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (checkBoxShowHourMinDTRPrint.CheckState == CheckState.Checked)
                    _config.AppSettings.Settings["Print-All"].Value = @"True";
                else
                {
                    _config.AppSettings.Settings["Print-All"].Value = @"False";
                }
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update Show Arrival and Departure Setting due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxUndertime_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["UndertimeLabel"].Value = waterMarkTextBoxUndertime.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update UndertimeLabel due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxOvertime_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["OvertimeLabel"].Value = waterMarkTextBoxUndertime.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update OvertimeLabel due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxRegular_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["RegularLabel"].Value = waterMarkTextBoxRegular.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update RegularLabel due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxGraceInPM_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["GraceIn-PM"].Value = waterMarkTextBoxGraceInPM.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update GraceIn-PM due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxGraceInOT_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["GraceIn-OT"].Value = waterMarkTextBoxGraceInOT.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update GraceIn-OT due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }

        private void waterMarkTextBoxSuper_Validated(object sender, EventArgs e)
        {

        }

        private void waterMarkTextBoxSuper_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                _config.AppSettings.Settings["Superintendent"].Value = waterMarkTextBoxSuper.Text;
                _config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Cannot update Superintendent Setting due to Windows file and folder security restriction.
                                 Kindly contact administrator for assistance.", @"Security Resctricion",
                     MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
