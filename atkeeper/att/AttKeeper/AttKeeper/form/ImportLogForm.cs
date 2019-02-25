using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JBiometric.Data;
using AttKeeper.core;
using AttKeeper.core.dtr;
using AttKeeper.data;
using AttKeeper.man.obj;

namespace AttKeeper.form
{
    public partial class ImportLogForm : Form
    {
        public User User { get; set; }
        enum InputOption
        {
            LAN,
            USB
        };

        private enum BioType
        {
            ZK,
            Realand
        };

        List<DatData> _listDat = new List<DatData>();

        InputOption _oInput ;
        private BioType _bioType;
        public ImportLogForm()
        {
            InitializeComponent();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            //if ((_oInput == InputOption.LAN) && (_bioType == BioType.ZK))
            //{

            //}
            //else if ((_oInput == InputOption.USB) && (_bioType == BioType.ZK))
            //{
                machineInstanceBindingSource.AddNew();
                var udisk = new UDisk();
                
                byte[] byDataBuf = null;
                int iLength;//length of the bytes to get from the data

                string sPIN2 = "";
                string sVerified = "";
                string sTime_second = "";
                string sDeviceID = "";
                string sStatus = "";
                string sWorkcode = "";

                openFileDialog1.Filter = "1_attlog(*.dat)|*.dat";
                openFileDialog1.FileName = "1_attlog.dat";//1 stands for one possible deviceid
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileStream stream = new FileStream(openFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                    byDataBuf = File.ReadAllBytes(openFileDialog1.FileName);
                    iLength = Convert.ToInt32(stream.Length);

                    int iStartIndex = 0;
                    int iOneLogLength;//the length of one line of attendence log
                    for (int i = iStartIndex; i < iLength - 2; i++)//modify by darcy on Dec.4 2009
                    {
                        if (byDataBuf[i] == 13 && byDataBuf[i + 1] == 10)
                        {
                            iOneLogLength = (i + 1) + 1 - iStartIndex;
                            byte[] bySSRAttLog = new byte[iOneLogLength];
                            Array.Copy(byDataBuf, iStartIndex, bySSRAttLog, 0, iOneLogLength);

                            udisk.GetAttLogFromDat(bySSRAttLog, iOneLogLength, out sPIN2, out sTime_second, out sDeviceID, out sStatus, out sVerified, out sWorkcode);

                            var dat = new DatData
                            {
                                Pin = sPIN2,
                                AttTime = sTime_second,
                                Status = sStatus,
                                Verify = sVerified,
                                Device = sDeviceID,
                                WorkCode = sWorkcode
                            };

                            _listDat.Add(dat);

                            bySSRAttLog = null;
                            iStartIndex += iOneLogLength;
                            iOneLogLength = 0;
                        }
                    }
                    stream.Close();
                }
                datDataBindingSource.DataSource = null;
                datDataBindingSource.DataSource = _listDat;

            //}
            //else if ((_oInput == InputOption.USB) && (_bioType == BioType.Realand))
            //{
                
            //}
            //else
            //{
            //    return;
            //}
            
        }

        private void ImportLogForm_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _oInput = InputOption.LAN;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _oInput = InputOption.USB;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _bioType = BioType.ZK;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _bioType = BioType.Realand;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            jTabWizard1.SelectTab(tabPage2);
            //if ((_oInput == InputOption.LAN) && (_bioType == BioType.ZK))
            //{
            //    datDataDataGridView.Visible = false;
            //    machineDataGridView.Visible = true;
            //}
            //else if ((_oInput == InputOption.USB) && (_bioType == BioType.ZK))
            //{
            //    datDataDataGridView.Visible = true;
            //    machineDataGridView.Visible = false;
            //}
            //else if ((_oInput == InputOption.USB) && (_bioType == BioType.Realand))
            //{
            //    datDataDataGridView.Visible = true;
            //    machineDataGridView.Visible = false;
            //}
            //else
            //{
            //    return;
            //}
        }

        private void jTabWizard1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabPage1)
            {
                label2.BorderStyle = BorderStyle.FixedSingle;
                label3.BorderStyle = BorderStyle.None;
            }
            else
            {
                label3.BorderStyle = BorderStyle.FixedSingle;
                label2.BorderStyle = BorderStyle.None;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var bSuccessfull = false;
            var setNme = String.Format(@"USB{0}", DateTime.Now);
            Application.DoEvents();
            Thread.Sleep(200);
            machineInsNameTextBox.Text = setNme;
            //save machine instance
            var iResult = SaveMachineInstance();
            if (iResult <= 0) return;
            var listLogs = DTRManagement.RemoveDuplicateUsb(_listDat, iResult);
            var iiResult = MachineManager.Save(listLogs);
            if (iiResult <= 0) return;
            List<MacDumpLog> listMacDump;
            var listDTR = DTRManagement.LoadEnrolleeAttendanceDtrAll(EmployeeManager.GetAll(true),
                out listMacDump, User.UserName);

            //save DTR
            var iiiResult = DTRManager.Save(listDTR);
            if (iiiResult > 0)
            {
                bSuccessfull = true;
            }

            //save macDump
            if (listMacDump.Count > 0)
            {
                var tempMacDump =  new List<MacDumpLog>();
                foreach (var dumpLog in listMacDump)
                {
                    if (dumpLog.MacDumpDate != null)
                    {
                        tempMacDump.Add(dumpLog);
                    }
                }
                var iiiiResult = MacDumpLogManager.Save(tempMacDump);
                if (iiiiResult > 0)
                {
                    Console.WriteLine(@"MacDump Successfully Record.");
                }
            }

            if (bSuccessfull)
            {
                MessageBox.Show(@"DTR Generated Successfully. Kindly Close the form and go to DTR Management",
                    @"Save - DTR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(@"Error occured during save.",
                    @"Error Save - DTR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int SaveMachineInstance()
        {
            Validate();
            machineInstanceBindingSource.EndEdit();
            return  MachineInstanceManager.Save((MachineInstance) machineInstanceBindingSource.Current);
        }
    }
}
