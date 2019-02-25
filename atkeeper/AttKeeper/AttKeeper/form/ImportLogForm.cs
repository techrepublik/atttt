using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using Excel=Microsoft.Office.Interop.Excel;

namespace AttKeeper.form
{
    public partial class ImportLogForm : Form
    {
        private static Excel.Workbook book = null;
        private static Excel.Application app = null;
        private Excel.Worksheet sheet = null;
        
        
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

        private InputOption _oInput ;
        private BioType _bioType;

        private int _option;

        public ImportLogForm()
        {
            InitializeComponent();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            machineInstanceBindingSource.AddNew();
            if ((radioButton2.Checked) && (radioButton3.Checked))
            {
                var udisk = new UDisk();
                
                byte[] byDataBuf = null;
                int iLength;//length of the bytes to get from the data

                string sPIN2 = "";
                string sVerified = "";
                string sTime_second = "";
                string sDeviceID = "";
                string sStatus = "";
                string sWorkcode = "";
                
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    openFileDialog1.Filter = @"zklog(*.dat)|*.dat";
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
                _option = 1;
            }
            else if ((radioButton2.Checked) && (radioButton4.Checked))
            {
                openFileDialog1.Filter = @"HisGLog_0001_20170801(*.csv)|*.csv";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var filename = openFileDialog1.FileName;
                    Console.WriteLine(filename);
                    string[] allLines = File.ReadAllLines(openFileDialog1.FileName);
                    var query = from line in allLines
                                let data = line.Split('\t')
                                select new DatData
                                {
                                   Pin = data[2],
                                   AttTime = data[8],
                                   Device = data[1],
                                   Status = data[4],
                                   Verify = data[5],
                                   WorkCode = data[7]
                                };

                    foreach (var item in query)
                    {
                        int iResult = 0;
                        if (int.TryParse(item.Pin, out iResult))
                            _listDat.Add(item);
                    }
                }
                datDataBindingSource.DataSource = _listDat;
                _option = 2;
            }
            else if ((radioButton2.Checked) && (radioButtonAnviz.Checked))
            {
                try
                {
                    System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\x.bat", Directory.GetCurrentDirectory() + @"\anviz\anviz_file\convert.js");

                    //command process - call to cmd and execute node.js
                    //var startInfo = new ProcessStartInfo
                    //{
                    //    FileName = "cmd.exe",
                    //    RedirectStandardInput = true,
                    //    RedirectStandardOutput = true,
                    //    UseShellExecute = false,
                    //    CreateNoWindow = false
                    //};
                    //var process = new Process { StartInfo = startInfo };
                    //process.Start();
                    //var filePath = Directory.GetCurrentDirectory() + @"\anviz\anviz_file\convert.js";
                    //process.StandardInput.WriteLine(@"node " + filePath);
                    //process.StandardInput.WriteLine("exit");
                    //process.WaitForExit();

                    //setting file and display the result
                    var filename = Directory.GetCurrentDirectory() + @"\anviz\anviz_file\bak.xlsx";
                    Console.WriteLine(filename);
                    app = new Excel.Application { Visible = false };
                    book = app.Workbooks.Open(filename);
                    sheet = (Excel.Worksheet)book.Sheets[1];
                    var lastRow = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
                    var tempList = new List<DatData>();
                    for (var i = 2; i < lastRow; i++)
                    {
                        var values = (System.Array)sheet.get_Range("A" + i.ToString(), "D" + i.ToString()).Cells.Value;
                        tempList.Add(new DatData
                        {
                            Pin = values.GetValue(1, 1).ToString(),
                            AttTime = values.GetValue(1, 2).ToString(),
                            Status = values.GetValue(1, 3).ToString(),
                            Device = "0",
                            Verify = "0",
                            WorkCode = "0"
                        });

                    }
                    book.Close(Type.Missing, Type.Missing, Type.Missing);
                    app.Quit();
                    foreach (var item in tempList)
                    {
                        int iResult = 0;
                        if (int.TryParse(item.Pin, out iResult))
                            _listDat.Add(item);
                    }
                    datDataBindingSource.DataSource = _listDat;
                    _option = 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Kindly check the error: " + ex.Message);
                }
            }
            else if ((radioButton2.Checked) && (radioButtonW.Checked))
            {
                openFileDialog1.Filter = @"GLog_001(*.txt)|*.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    var filename = openFileDialog1.FileName;
                    Console.WriteLine(filename);
                    string[] allLines = File.ReadAllLines(openFileDialog1.FileName);
                    var query = from line in allLines
                                let data = line.Split('\t')
                                select new DatData
                                {
                                    Pin = data[2],
                                    AttTime = data[6],
                                    Device = data[1],
                                    Status = data[4],
                                    Verify = data[5],
                                    WorkCode = data[5]
                                };

                    foreach (var item in query)
                    {
                        int iResult = 0;
                        if (int.TryParse(item.Pin, out iResult))
                            _listDat.Add(item);
                    }
                }
                datDataBindingSource.DataSource = _listDat;
                _option = 2;
            }
            else if ((radioButton1.Checked) && (radioButton3.Checked))
            {
                int connectError = 0;

                
            }
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
            toolStripStatusLabel1.Text = @"Source: ZKTech Bio-device";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _bioType = BioType.Realand;
            toolStripStatusLabel1.Text = @"Source: Realand Bio-device";
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
            toolStripStatusLabel1.Text = @"Processing Data...";
            var bSuccessfull = false;
            var setNme = String.Format(@"USB{0}", DateTime.Now);
            Application.DoEvents();
            Thread.Sleep(200);
            machineInsNameTextBox.Text = setNme;

            //save machine instance
            var iResult = SaveMachineInstance();
            if (iResult <= 0) return;
            var listLogs = DTRManagement.RemoveDuplicateUsb(_listDat, iResult, _option);
            toolStripStatusLabel1.Text = @"Removing duplicates and saving...";
            Application.DoEvents();
            Thread.Sleep(200);
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
            toolStripStatusLabel1.Text = @"Done.";
        }

        private int SaveMachineInstance()
        {
            Validate();
            machineInstanceBindingSource.EndEdit();
            return  MachineInstanceManager.Save((MachineInstance) machineInstanceBindingSource.Current);
        }

        private void radioButtonAnviz_CheckedChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = @"Source: Anviz Bio-device";
        }
    }
}
