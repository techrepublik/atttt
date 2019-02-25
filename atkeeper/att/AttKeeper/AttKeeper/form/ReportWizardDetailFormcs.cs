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
using Microsoft.Reporting.WinForms;

namespace AttKeeper.form
{
    public partial class ReportWizardDetailFormcs : Form
    {
        public ReportWizardDetailFormcs()
        {
            InitializeComponent();
        }

        private void ReportWizardDetailFormcs_Load(object sender, EventArgs e)
        {

            
        }

        public void LoadData(List<DtrData> listDTR)
        {
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            JDTRBindingSource.DataSource = listDTR;
            this.reportViewer1.RefreshReport();
        }
    }
}
