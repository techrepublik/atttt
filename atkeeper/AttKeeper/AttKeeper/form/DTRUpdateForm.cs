using System;
using System.Windows.Forms;
using AttKeeper.data;
using AttKeeper.man.obj;

namespace AttKeeper.form
{
    public partial class DTRUpdateForm : Form
    {
        public int IEnrolleeId { get; set; }
        public int IEnrolleeNo { get; set; }
        public string SDate { get; set; }
        public DTRUpdateForm()
        {
            InitializeComponent();
        }

        private void DTRUpdateForm_Load(object sender, EventArgs e)
        {
            buttonOk.Enabled = false;
            macDumpLogBindingSource.DataSource = MacDumpLogManager.GetAll(IEnrolleeNo.ToString(), SDate);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            
            if ((macDumpLogBindingSource != null) && (macDumpLogBindingSource.Count > 0))
                DTRForm.STime = ((MacDumpLog)macDumpLogBindingSource.Current).MacDumpTime;

            Close();
        }

        private void macDumpLogBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (macDumpLogBindingSource?.Current == null) return;
            if (macDumpLogBindingSource.Count <= 0) return;
            buttonOk.Enabled = true;
        }
    }
}
