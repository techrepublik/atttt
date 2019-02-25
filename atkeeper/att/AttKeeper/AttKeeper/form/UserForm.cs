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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void userBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            userBindingSource.EndEdit();
            var iResult = UserManager.Save((User) userBindingSource.Current);
            if (iResult > 0)
            {
                MessageBox.Show(@"Record was successfully saved.", @"Save", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                userNameTextBox.Focus();
            }
            else
            {
                MessageBox.Show(@"Error occurred in saving.", @"Save", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteUser();
        }
        private void DeleteUser()
        {
            if (userBindingSource == null) return;
            var dResult = MessageBox.Show(@"Delete current record?", @"Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dResult != DialogResult.Yes) return;
            if (UserManager.Delete(((User)userBindingSource.Current).UserId))
            {
                MessageBox.Show(@"Record was deleted successfully.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                userBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show(@"Error on delete operation.", @"Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                userNameTextBox.Focus();
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            userNameTextBox.Focus();
        }

        private void userBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (userBindingSource != null)
                userBindingNavigatorSaveItem.Enabled = true;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void LoadUser()
        {
            userBindingSource.DataSource = UserManager.GetAll();
        }
    }
}
