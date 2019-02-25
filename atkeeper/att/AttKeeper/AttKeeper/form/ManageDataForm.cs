using System;
using System.Linq;
using System.Windows.Forms;
using AttKeeper.data;
using AttKeeper.man.obj;
using UtilityManager.util;

namespace AttKeeper.form
{
    public partial class ManageDataForm : Form
    {

        private enum EChoice
        {
            Employee,
            Leave
        };

        private EChoice _choice;
        public ManageDataForm()
        {
            InitializeComponent();
        }

        private void ManageDataForm_Load(object sender, EventArgs e)
        {
            InitRecord();
            InitComboBox();
        }

        private void InitRecord()
        {
            Cursor.Current = Cursors.WaitCursor;
            empoyeeBindingSource.DataSource = EmployeeManager.GetAll();
            Cursor.Current = Cursors.Default;
        }
        private void empoyeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (CheckEmptyTextBoxes())
            {
                if (empoyeeBindingSource == null) return;
                Validate();
                if (employeePhotoPictureBox.Image != null)
                    ((Empoyee) empoyeeBindingSource.Current).EmployeePhoto =
                        ImageClass.ImageToByte(employeePhotoPictureBox.Image);
                empoyeeBindingSource.EndEdit();
                var iResult = EmployeeManager.Save((Empoyee) empoyeeBindingSource.Current);
                if (iResult > 0)
                    MessageBox.Show(@"Record successfully saved.", @"Save", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show(@"Error in saving.", @"Save", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(@"Please fill-out required field.", @"Required", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void employeeNoTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeNoTextBox, true);
        }

        private void employeeNoTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeNoTextBox, false);
        }

        private void employeeIdNoTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeIdNoTextBox, true);
        }

        private void employeeIdNoTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeIdNoTextBox, false);
        }

        private void employeeFirstNameTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeFirstNameTextBox, true);
        }

        private void employeeFirstNameTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeFirstNameTextBox, false);
        }

        private void employeeMiddleNameTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeMiddleNameTextBox, true);
        }

        private void employeeMiddleNameTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeMiddleNameTextBox, false);
        }

        private void employeeLastNameTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeLastNameTextBox, true);
        }

        private void employeeLastNameTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeLastNameTextBox, false);
        }

        private void employeeAddressTextBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeAddressTextBox, true);
        }

        private void employeeAddressTextBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeAddressTextBox, false);
        }

        private void employeeSexComboBox_Enter(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeSexComboBox, true);
        }

        private void employeeSexComboBox_Leave(object sender, EventArgs e)
        {
            UtilClass.ChangeColor(employeeSexComboBox, false);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            employeeNoTextBox.Focus();
            ((Empoyee) empoyeeBindingSource.Current).EmployeeIsActive = true;
            employeeIsActiveCheckBox.Checked = true;
        }

        private void empoyeeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (empoyeeBindingSource == null) return;
            empoyeeBindingNavigatorSaveItem.Enabled = true;
            if (((Empoyee)empoyeeBindingSource.Current).EmployeePhoto != null)
            {
                employeePhotoPictureBox.Image = ImageClass.ByteToImage(((Empoyee)empoyeeBindingSource.Current).EmployeePhoto.ToArray());
            }
            LoadInitLeaves();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = @"Jpeg (*.jpg)|*.jpg|Jpeg (*.jpeg)|*.jpeg|Png (*.png) |*.png|Gif (*.gif) | *.gif";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                employeePhotoPictureBox.ImageLocation = openDialog.FileName;
                byte[] tempBuffer = ImageClass.ReadFile(openDialog.FileName);
                employeePhotoPictureBox.Image = ImageClass.ByteToImage(tempBuffer);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            switch (_choice)
            {
                case EChoice.Employee:
                    DeleteEmployee();
                    break;
                case EChoice.Leave:
                    DeleteLeave();
                    break;
                default:
                    break;
            }
        }

        private void DeleteEmployee()
        {
            if (empoyeeBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record? - Employee", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (EmployeeManager.Delete(((Empoyee)empoyeeBindingSource.Current).EmployeeId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                empoyeeBindingSource.RemoveCurrent();    
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                employeeNoTextBox.Focus();
            }
        }
        private void DeleteLeave()
        {
            if (leafBindingSource?.Current == null) return;
            var dResult = MessageBox.Show(@"Delete current record? - Leave", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (LeaveManager.Delete(((Leaf)leafBindingSource.Current).LeaveId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                leafBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                leafDataGridView.Focus();
            }
        }

        private void InitComboBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            settingBindingSource.DataSource = SettingManager.GetAll();
            positionBindingSource.DataSource = PositionManager.GetAll();
            leaveTypeBindingSource.DataSource = LeaveTypeManager.GetAll();
            Cursor.Current = Cursors.Default;
        }

        private void leafDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if(leafBindingSource?.Current == null) return;
            if (leafDataGridView.Rows.Count <= 1) return;
            if (!leafDataGridView.IsCurrentRowDirty) return;
            Validate();
            ((Leaf) leafBindingSource.Current).EmployeeId = ((Empoyee) empoyeeBindingSource.Current).EmployeeId;
            leafBindingSource.EndEdit();
            var iResult = LeaveManager.Save((Leaf)leafBindingSource.Current);
            if (iResult > 0) LoadInitLeaves();
        }

        private void LoadInitLeaves()
        {
            Cursor.Current = Cursors.WaitCursor;
            leafBindingSource.DataSource = LeaveManager.GetAll(((Empoyee)empoyeeBindingSource.Current).EmployeeId);
            Cursor.Current = Cursors.Default;
        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    _choice = EChoice.Employee;
                    break;
                case 1:
                    _choice = EChoice.Leave;
                    break;
                default:
                    break;
            }
        }

        private bool CheckEmptyTextBoxes()
        {
            return ((employeeNoTextBox.Text.Trim().Length > 0) && (employeeIdNoTextBox.Text.Trim().Length > 0) 
                && (employeeFirstNameTextBox.Text.Trim().Length > 0) && (employeeMiddleNameTextBox.Text.Trim().Length > 0) 
                && (employeeLastNameTextBox.Text.Trim().Length > 0) && (settingIdComboBox.SelectedIndex >= 0) && (positionIdComboBox.SelectedIndex >= 0));
        }
    }
}
