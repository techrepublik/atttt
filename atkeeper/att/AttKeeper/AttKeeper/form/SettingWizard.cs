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
using AttKeeper.man;
using AttKeeper.man.obj;

namespace AttKeeper.form
{
    public partial class SettingWizard : Form
    {

        private bool _bSet = false;
        public SettingWizard()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(jTabWizard1.SelectedIndex <= 0) return;
            jTabWizard1.SelectTab(jTabWizard1.SelectedIndex - 1);
            selectTab();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (jTabWizard1.SelectedIndex >= jTabWizard1.TabCount - 1) return;
            jTabWizard1.SelectTab(jTabWizard1.SelectedIndex + 1);
            selectTab();
        }

        private void selectTab()
        {
            switch (jTabWizard1.SelectedIndex)
            {
                case 0:
                    label20.BorderStyle = BorderStyle.FixedSingle;
                    label2.BorderStyle = BorderStyle.None;
                    break;
                case 1:
                    label20.BorderStyle = BorderStyle.None;
                    label2.BorderStyle = BorderStyle.FixedSingle;
                    break;
                default:
                    break;
            }
        }

        private void SettingWizard_Load(object sender, EventArgs e)
        {
            LoadInitData();
        }

        private void LoadInitData()
        {
            Cursor.Current = Cursors.WaitCursor;
            settingBindingSource.DataSource = SettingManager.GetAll();
            Cursor.Current = Cursors.Default;
        }

        private void LoadDetailData(int iSettingId)
        {
            Cursor.Current = Cursors.WaitCursor;
            settingDetailBindingSource.DataSource = SettingDetailManager.GetAll(iSettingId);
            Cursor.Current = Cursors.Default;
        }

        private void jTabWizard1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0:
                    //buttonNext.Enabled = guestNameTextBox.Text.Length > 0;
                    break;
                case 1:
                    //label5.Text = guestNameTextBox.Text.ToUpper();
                    break;
                default:
                    break;
            }
        }

        private void settingBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (settingBindingSource == null) return;
            var iSettingId = ((Setting) settingBindingSource.Current).SettingId;
            if (iSettingId > 0) LoadDetailData(iSettingId);
        }

        private void settingDataGridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (settingBindingSource == null) return;
            if (settingDataGridView.Rows.Count <= 1) return;
            if (!settingDataGridView.IsCurrentRowDirty) return;
            Validate();
            settingBindingSource.EndEdit();
            var iResult = SettingManager.Save((Setting)settingBindingSource.Current);
            if (iResult > 0) LoadInitData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            settingDetailBindingSource.AddNew();
            settingDetailDayComboBox.SelectedIndex = 0;
            settingDetailDayComboBox.Focus();
        }

        private void settingDetailBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            _bSet = true;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (settingBindingSource?.Current == null) return;
            Validate();
            ((SettingDetail)settingDetailBindingSource.Current).SettingId =
                ((Setting)settingBindingSource.Current).SettingId;
            settingDetailBindingSource.EndEdit();
            var iResult = SettingDetailManager.Save((SettingDetail)settingDetailBindingSource.Current);
            if (iResult > 0)
                MessageBox.Show(@"Record Successfully saved.", @"Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            DeleteSettingDetail();
        }

        private void DeleteSettingDetail()
        {
            if (settingDetailBindingSource != null)
            {
                var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dResult == DialogResult.Yes)
                {
                    if (SettingDetailManager.Delete(((SettingDetail)settingDetailBindingSource.Current).SettingDetailId))
                    {
                        MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        settingDetailBindingSource.RemoveCurrent();
                    }
                    else
                    {
                        MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        settingDetailDayComboBox.Focus();
                    }
                }
            }
        }

        private void settingDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DeleteSetting();
        }
        private void DeleteSetting()
        {
            if (settingBindingSource != null)
            {
                var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dResult == DialogResult.Yes)
                {
                    if (SettingManager.Delete(((Setting)settingBindingSource.Current).SettingId))
                    {
                        MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        settingBindingSource.RemoveCurrent();
                    }
                    else
                    {
                        MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        settingDataGridView.Focus();
                    }
                }
            }
        }
    }
}
