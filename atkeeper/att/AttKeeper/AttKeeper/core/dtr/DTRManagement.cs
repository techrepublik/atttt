using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AttKeeper.data;
using JBiometric.Entities;
using AttKeeper.man.obj;
using AttKeeper.core.dtr;

namespace AttKeeper.core.dtr
{
    class DTRManagement
    {
        #region LoadDTR
        public static List<DTR> LoadDTRViaDTR(Empoyee enrollee, int iMonth, int iYear, List<DTR> lDTRs, out string sSetting)
        {
            var lDT = new List<DTR>();
            //load setting detail
            var lSetDeatils = new List<SettingDetail>();
            sSetting = null;
            //load leave
            var dLeave = LoadLeaveDictionary(enrollee.EmployeeId, iMonth, iYear);
            //var Misc = oMisc;
            var graceInAm = ConfigurationManager.AppSettings["GraceIn-AM"];
            var graceInPm = ConfigurationManager.AppSettings["GraceIn-PM"];
            var graceInOt = ConfigurationManager.AppSettings["GraceIn-OT"];

            var regular = ConfigurationManager.AppSettings["RegularLabel"];
            var undertime = ConfigurationManager.AppSettings["UndertimeLabel"];
            var overtime = ConfigurationManager.AppSettings["OvertimrLabel"];
            //get setting for employee
            lSetDeatils = SettingDetailManager.GetAll(Convert.ToInt16(enrollee.SettingId));
            var iDaysMonth = DataTimeClass.GetDaysInMonth(iYear, iMonth);
            for (var i = 1; i <= iDaysMonth; i++)
            {
                DTR dtrTemp01 = new DTR();
                DateTime dtTemp = Convert.ToDateTime(iMonth.ToString() + "/" + i.ToString() + "/" + iYear.ToString());
                DTR dtrTemp = lDTRs.FirstOrDefault(d => d.DTRDate == dtTemp);
                string sDay = DataTimeClass.VerifyDay(dtTemp);
                string sHolidayTemp = string.Empty;
                TimeSpan? iTimeAM = new TimeSpan();
                TimeSpan? iTimePM = new TimeSpan();
                TimeSpan? iTimeOT = new TimeSpan();
                double? iTotalTime;

                if (dtrTemp != null)
                {
                    double? iTotalTimeAm = 0;
                    double? iTotalTimePM = 0;
                    double? iTotalTimeOt = 0;

                    dtrTemp01 = dtrTemp;
                    dtrTemp01.DTRDay = sDay;
                    foreach (var detail in lSetDeatils)
                    {
                        if (detail.SettingDetailDay.Trim() == sDay.Trim())
                        {
                            DateTime? dtTimeInAM = null;
                            DateTime? dtTimeOutAM = null;
                            DateTime? dtTimeInPM = null;
                            DateTime? dtTimeOutPM = null;
                            DateTime? dtTimeInOT = null;
                            DateTime? dtTimeOutOT = null;

                            double dOver = 0;

                            if (dtrTemp.TimeInAM != null)
                            {
                                DateTime tempTimeInAM;
                                if (DateTime.TryParse(dtrTemp.TimeInAM, out tempTimeInAM))
                                {
                                    //dtTimeInAM = Convert.ToDateTime(dtrTemp.TimeInAM);
                                    dtTimeInAM = tempTimeInAM;
                                    //if (Misc.MiscActive == true)
                                    //{
                                        string shortTimeString = detail.SettingDetailINAM.Value.ToShortTimeString();
                                        DateTime tempSetTime = Convert.ToDateTime(shortTimeString);

                                        //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                        if (DataTimeClass.IsCompareTime((DateTime)dtTimeInAM, tempSetTime, 0, Convert.ToDouble(graceInAm), out dOver))
                                        {
                                            DateTime tempTime = dOver > 0 ? dtTimeInAM.Value.AddMinutes(-Convert.ToDouble(graceInAm)) : tempSetTime;
                                            //DateTime tempTime =
                                            //    dtTimeInAM.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInAM));

                                            //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                            //{
                                            //dtTimeInAM = (Misc.MiscGraceInAM <= tempTime.Minute)
                                            //                 ? tempTime
                                            //                 : tempSetTime;

                                            dtTimeInAM = (tempTime.Minute > 0)
                                                                 ? tempTime
                                                                 : tempSetTime;
                                            //}
                                        }
                                        else
                                        {
                                            dtTimeInAM = tempSetTime;
                                        }
                                    //}
                                    //else
                                    //{
                                    //    string shortTimeString = detail.SettingDetailINAM.Value.ToShortTimeString();
                                    //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                    //    dtTimeInAM = DataTimeClass.IsCompareTime(dtTimeInAM.Value, tempSetTime, 0, Convert.ToDouble(Misc.MiscGraceInAM), out dOver) ? dtTimeInAM : tempSetTime;
                                    //}
                                }
                                else
                                {
                                    dtTimeInAM = null;
                                }
                            }

                            if (dtrTemp.TimeOutAM != null)
                            {
                                DateTime tempTimeOutAm;
                                if (DateTime.TryParse(dtrTemp.TimeOutAM, out tempTimeOutAm))
                                {
                                    //dtTimeOutAM = Convert.ToDateTime(dtrTemp.TimeOutAM);
                                    dtTimeOutAM = tempTimeOutAm;
                                    string shortTimeString = detail.SettingDetailOUTAM.Value.ToShortTimeString();
                                    DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                    dtTimeOutAM = DataTimeClass.IsCompareTime((DateTime)dtTimeOutAM, dtShort, 1, 0, out dOver)
                                                      ? dtTimeOutAM
                                                      : dtShort;
                                }
                            }

                            if (dtrTemp.TimeInPM != null)
                            {
                                DateTime tempTimeInPm;
                                if (DateTime.TryParse(dtrTemp.TimeInPM, out tempTimeInPm))
                                {
                                    //dtTimeInPM = Convert.ToDateTime(dtrTemp.TimeInPM);
                                    dtTimeInPM = tempTimeInPm;
                                    //if (Misc.MiscActive == true)
                                    //{
                                        string shortTimeString = detail.SettingDetailINPM.Value.ToShortTimeString();
                                        DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                        //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                        if (DataTimeClass.IsCompareTime((DateTime)dtTimeInPM, tempSetTime, 0, Convert.ToDouble(graceInPm), out dOver))
                                        {

                                            //this is the culprit --------->>>>
                                            //DateTime tempTime =
                                            //    dtTimeInPM.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInPM));
                                            DateTime tempTime = dOver > 0 ? dtTimeInPM.Value.AddMinutes(-Convert.ToDouble(graceInPm)) : tempSetTime;
                                            //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                            //{
                                            //dtTimeInPM = (Misc.MiscGraceInPM <= tempTime.Minute)
                                            //                 ? tempTime
                                            //                 : tempSetTime;

                                            dtTimeInPM = (tempTime.Minute > 0)
                                                                ? tempTime
                                                                : tempSetTime;
                                            //}
                                        }
                                        else
                                        {
                                            dtTimeInPM = tempSetTime;
                                        }
                                    //}
                                    //else
                                    //{
                                    //    string shortTimeString = detail.SettingDetailINPM.Value.ToShortTimeString();
                                    //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                    //    dtTimeInPM = DataTimeClass.IsCompareTime(dtTimeInPM.Value, tempSetTime, 0, Convert.ToDouble(Misc.MiscGraceInPM), out dOver)
                                    //                     ? dtTimeInPM
                                    //                     : tempSetTime;
                                    //}
                                }
                                else
                                {
                                    dtTimeInPM = null;
                                }
                            }
                            if (dtrTemp.TimeOutPM != null)
                            {
                                DateTime tempTimeOutPm;
                                if (DateTime.TryParse(dtrTemp.TimeOutPM, out tempTimeOutPm))
                                {
                                    //dtTimeOutPM = Convert.ToDateTime(dtrTemp.TimeOutPM);
                                    dtTimeOutPM = tempTimeOutPm;
                                    string shortTimeString = detail.SettingDetailOUTPM.Value.ToShortTimeString();
                                    DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                    dtTimeOutPM = DataTimeClass.IsCompareTime((DateTime)dtTimeOutPM, dtShort, 1, 0, out dOver)
                                                      ? dtTimeOutPM
                                                      : dtShort;
                                }
                            }

                            if (dtrTemp.TimeInOT != null)
                            {
                                DateTime tempTimeInOt;
                                if (DateTime.TryParse(dtrTemp.TimeInOT, out tempTimeInOt))
                                {
                                    //dtTimeInOT = Convert.ToDateTime(dtrTemp.TimeInOT);
                                    dtTimeInOT = tempTimeInOt;
                                    //if (Misc.MiscActive == true)
                                    //{
                                        string shortTimeString = detail.SettingDetailINOT.Value.ToShortTimeString();
                                        DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                        //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                        if (DataTimeClass.IsCompareTime((DateTime)dtTimeInOT, tempSetTime, 0, Convert.ToDouble(graceInOt), out dOver))
                                        {
                                            //DateTime tempTime =
                                            //    dtTimeInOT.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInOT));
                                            DateTime tempTime = dOver > 0 ? dtTimeInOT.Value.AddMinutes(-Convert.ToDouble(graceInOt)) : dtTimeInOT.Value;
                                            //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                            //{
                                            dtTimeInOT = (Convert.ToDouble(graceInOt) <= tempTime.Minute)
                                                             ? tempTime
                                                             : tempSetTime;
                                            //}
                                        }
                                        else
                                        {
                                            dtTimeInOT = tempSetTime;
                                        }
                                    //}
                                    //else
                                    //{
                                    //    string shortTimeString = detail.SettingDetailINOT.Value.ToShortTimeString();
                                    //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                    //    dtTimeInOT = DataTimeClass.IsCompareTime(dtTimeInOT.Value, tempSetTime, 0, Convert.ToDouble(Misc.MiscGraceOT), out dOver)
                                    //                     ? dtTimeInPM
                                    //                     : tempSetTime;
                                    //}
                                }
                                else
                                {
                                    dtTimeInOT = null;
                                }
                            }

                            if (dtrTemp.TimeOutOT != null)
                            {
                                DateTime tempTimeOutOt;
                                if (DateTime.TryParse(dtrTemp.TimeOutOT, out tempTimeOutOt))
                                {
                                    //dtTimeOutOT = Convert.ToDateTime(dtrTemp.TimeOutOT);
                                    dtTimeOutOT = tempTimeOutOt;
                                    string shortTimeString = detail.SettingDetailOUTPM.Value.ToShortTimeString();
                                    DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                    dtTimeOutOT = DataTimeClass.IsCompareTime((DateTime)dtTimeOutOT, dtShort, 1, 0, out dOver)
                                                      ? dtTimeOutOT
                                                      : dtShort;
                                }
                            }

                            DateTime? tempDtInAm = null;
                            if (dtTimeInAM != null)
                                tempDtInAm = Convert.ToDateTime(dtTimeInAM.Value.ToString("HH:mm"));

                            DateTime? tempDtOutAm = null;
                            if (dtTimeOutAM != null)
                                tempDtOutAm = Convert.ToDateTime(dtTimeOutAM.Value.ToString("HH:mm"));

                            DateTime? tempDtInOt = null;
                            if (dtTimeInOT != null)
                                Convert.ToDateTime(dtTimeInOT.Value.ToString("HH:mm"));

                            DateTime? tempDtOutOT = null;
                            if (dtTimeOutOT != null)
                                Convert.ToDateTime(dtTimeOutOT.Value.ToString("HH:mm"));

                            iTimeAM = tempDtOutAm - tempDtInAm; // dtTimeOutAM.Subtract(dtTimeInAM);
                            iTimePM = dtTimeOutPM - dtTimeInPM;
                            iTimeOT = tempDtOutOT - tempDtInOt;
                        }
                    }

                    double? iHourAm = null;
                    if (iTimeAM != null)
                        iHourAm = iTimeAM.Value.Hours;

                    double? iMinAM = null;
                    if (iTimeAM != null)
                        iMinAM = iTimeAM.Value.Minutes;

                    iTotalTimeAm = (iHourAm + (iMinAM / 60));

                    double? iHourPm = null;
                    if (iTimePM != null)
                        iHourPm = iTimePM.Value.Hours;

                    double? iMinPM = null;
                    if (iTimePM != null)
                        iMinPM = iTimePM.Value.Minutes;

                    iTotalTimePM = (iHourPm + (iMinPM / 60));

                    double? iHourOt = 0;
                    if (iTimeOT != null)
                        iHourOt = iTimeOT.Value.Hours;

                    double? iMinOt = 0;
                    if (iMinOt != 0)
                        iMinOt = iTimeOT.Value.Minutes;

                    iTotalTimeOt = (iHourOt + (iMinOt / 60));

                    //if (Misc.MiscActive == true)
                    //{
                        //double dHour = Convert.ToDouble(Misc.MiscRegularHours);
                        double dHour = 8;
                        iTotalTime = iTotalTimeAm + iTotalTimePM + iTotalTimeOt;

                        dtrTemp01.TotalHour = iTotalTime;

                        //Computer for hour
                        dtrTemp01.TotalHour = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                              (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                  ? ((iHourAm + iHourPm))
                                                  : 0;

                        dtrTemp01.TotalHour = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                  ? ((dtrTemp01.TotalHour + iHourOt))
                                                  : (dtrTemp01.TotalHour);


                        //Compute for minutes
                        dtrTemp01.TotalMinute = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                    ? (iMinAM + iMinPM)
                                                    : 0;

                        dtrTemp01.TotalMinute = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                    ? (dtrTemp01.TotalMinute + iMinOt)
                                                    : dtrTemp01.TotalMinute;


                        if (dtrTemp01.TotalMinute > 0)
                        {
                            if (dtrTemp01.TotalMinute >= 60)
                            {
                                int tempToHour = Convert.ToInt32(Convert.ToDouble(dtrTemp01.TotalMinute) / 60);
                                int tempToMin = Convert.ToInt32((Convert.ToDouble(dtrTemp01.TotalMinute) % 60));
                                dtrTemp01.TotalHour = (dtrTemp01.TotalHour + tempToHour) - dHour;
                                dtrTemp01.TotalMinute = 60 - tempToMin;
                            }
                            else
                            {
                                dtrTemp01.TotalHour = (dtrTemp01.TotalHour + 1) - dHour;
                                double? dMinTotal = 60 - dtrTemp01.TotalMinute;
                                dtrTemp01.TotalMinute = dMinTotal;
                            }
                        }

                        //Compute for total hours
                        double? tempITotaltime = iTotalTime - dHour;
                        dtrTemp01.TotalHours = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                               (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                   ? tempITotaltime
                                                   : 0;
                        dtrTemp01.TotalHours = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                   ? (dtrTemp01.TotalHours + iTotalTimeOt)
                                                   : dtrTemp01.TotalHours;

                        if (dtrTemp01.TotalHours == 0)
                        {
                            //dtrTemp01.DTRStatus = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                            //                      (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                            //                       ? Misc.MiscRegularLabel
                            //                       : "-";
                            dtrTemp01.DTRStatus = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                  (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                   ? regular
                                                   : "-";
                            dtrTemp01.TotalHour = null;
                            dtrTemp01.TotalMinute = null;
                        }
                        else if (dtrTemp01.TotalHours > 0)
                        {
                            dtrTemp01.DTRStatus = overtime;
                            //dtrTemp01.TotalHour = dtrTemp01.TotalHours;
                            //dtrTemp01.TotalMinute = 0;
                            dtrTemp.TotalHour = null;
                            dtrTemp.TotalMinute = null;
                            dtrTemp.TotalHours = null;
                        }
                        else if (dtrTemp01.TotalHours < 0)
                        {
                            dtrTemp01.DTRStatus = undertime;
                            //dtrTemp01.TotalMinute = dtrTemp01.TotalHours;
                            //dtrTemp01.TotalHour = 0;
                        }
                        else
                        {
                            dtrTemp01.DTRStatus = "-";
                            //dtrTemp01.TotalHour = 0;
                            //dtrTemp01.TotalMinute = 0;
                        }
                    //}
                    //else
                    //{
                    //    iTotalTime = iTotalTimeAM + iTotalTimePM + iTotalTimeOT;
                    //    dtrTemp01.TotalHours = iTotalTime;
                    //}

                    if (IsHoliday(dtTemp, out sHolidayTemp))
                        dtrTemp01.DTRStatus = "(h)" + sHolidayTemp;

                    if (dLeave.ContainsKey(dtTemp))
                    {
                        var tempStatus = dLeave[dtTemp];
                        //A.M.
                        if (dtrTemp01.TimeInAM == null)
                            dtrTemp01.TimeInAM = tempStatus;
                        else if (dtrTemp01.TimeInAM.Trim().Length == 0)
                            dtrTemp01.TimeInAM = tempStatus;

                        if (dtrTemp01.TimeOutAM == null)
                            dtrTemp01.TimeOutAM = tempStatus;
                        else if (dtrTemp01.TimeOutAM.Trim().Length == 0)
                            dtrTemp01.TimeOutAM = tempStatus;

                        //P.M.
                        if (dtrTemp01.TimeInPM == null)
                            dtrTemp01.TimeInPM = tempStatus;
                        else if (dtrTemp01.TimeInPM.Trim().Length == 0)
                            dtrTemp01.TimeInPM = tempStatus;

                        if (dtrTemp01.TimeOutPM == null)
                            dtrTemp01.TimeOutPM = tempStatus;
                        else if (dtrTemp01.TimeOutPM.Trim().Length == 0)
                            dtrTemp01.TimeOutPM = tempStatus;

                        ////if (dtrTemp01.TimeInAM == null)
                        ////    dtrTemp01.TimeInAM = "L/O.B.";
                        ////else if (dtrTemp01.TimeInAM.Trim().Length == 0)
                        ////    dtrTemp01.TimeInAM = "L/O.B.";

                        ////if (dtrTemp01.TimeOutAM == null)
                        ////    dtrTemp01.TimeOutAM = "L/O.B.";
                        ////else if (dtrTemp01.TimeOutAM.Trim().Length == 0)
                        ////    dtrTemp01.TimeOutAM = "L/O.B";

                        //////P.M.
                        ////if (dtrTemp01.TimeInPM == null)
                        ////    dtrTemp01.TimeInPM = "L/O.B.";
                        ////else if (dtrTemp01.TimeInPM.Trim().Length == 0)
                        ////    dtrTemp01.TimeInPM = "L/O.B";

                        ////if (dtrTemp01.TimeOutPM == null)
                        ////    dtrTemp01.TimeOutPM = "L/O.B.";
                        ////else if (dtrTemp01.TimeOutPM.Trim().Length == 0)
                        ////    dtrTemp01.TimeOutPM = "L/O.B";

                        //O.T.
                        //if (dtrTemp01.TimeInOT == null)
                        //    dtrTemp01.TimeInOT = "L/O.B.";
                        //else if (dtrTemp01.TimeInOT.Trim().Length == 0)
                        //    dtrTemp01.TimeInOT = "L/O.B";

                        //if (dtrTemp01.TimeOutOT == null)
                        //    dtrTemp01.TimeOutOT = "L/O.B.";
                        //else if (dtrTemp01.TimeOutOT.Trim().Length == 0)
                        //    dtrTemp01.TimeOutOT = "L/O.B";

                        dtrTemp01.DTRStatus = dLeave[dtTemp];
                    }
                }
                else
                {
                    if ((sDay == "Sunday") || (sDay == "Saturday"))
                    {
                        dtrTemp01.TimeInAM = sDay;
                        dtrTemp01.TimeOutAM = "-----";
                        dtrTemp01.TimeInPM = sDay;
                        dtrTemp01.TimeOutPM = "-----";
                        dtrTemp01.TimeInOT = sDay;
                        dtrTemp01.TimeOutOT = "-----";
                    }
                    //else
                    //{
                    //    dtrTemp01.TimeInAM = "-";
                    //    dtrTemp01.TimeOutAM = "-";
                    //    dtrTemp01.TimeInPM = "-";
                    //    dtrTemp01.TimeOutPM = "-";
                    //    dtrTemp01.TimeInOT = "-";
                    //    dtrTemp01.TimeOutOT = "-";
                    //}
                    dtrTemp01.DTRDate = dtTemp;
                    dtrTemp01.DTRDay = sDay;
                    //dtrTemp01.TotalHours = Convert.ToDouble(iTotalTime);

                    if (IsHoliday(dtTemp, out sHolidayTemp))
                    {
                        dtrTemp01.DTRStatus = sHolidayTemp + " (h)";
                        dtrTemp01.TimeInAM = "Holid";
                        dtrTemp01.TimeOutAM = "-----";
                        dtrTemp01.TimeInPM = "Holid";
                        dtrTemp01.TimeOutPM = "-----";
                        dtrTemp01.TimeInOT = "Holid";
                        dtrTemp01.TimeOutOT = "-----";
                    }

                    if (dLeave.ContainsKey(dtTemp))
                    {
                        //dtrTemp01.DTRStatus = dLeave[dtTemp];
                        //dtrTemp01.TimeInAM = "L/O.B.";
                        //dtrTemp01.TimeOutAM = "-----";
                        //dtrTemp01.TimeInPM = "L/O.B.";
                        //dtrTemp01.TimeOutPM = "-----";
                        //dtrTemp01.TimeInOT = "L/O.B.";
                        //dtrTemp01.TimeOutOT = "-----";
                        var tempStatus = dLeave[dtTemp];
                        dtrTemp01.DTRStatus = tempStatus;
                        dtrTemp01.TimeInAM = tempStatus;
                        dtrTemp01.TimeOutAM = "-----";
                        dtrTemp01.TimeInPM = tempStatus;
                        dtrTemp01.TimeOutPM = "-----";
                        dtrTemp01.TimeInOT = tempStatus;
                        dtrTemp01.TimeOutOT = "-----";
                    }
                }

                var setting = string.Empty;
                foreach (var detail in lSetDeatils)
                {
                    setting = string.Format(@"AM [{0:t}-{1:t}], PM [{2:t}-{3:t}]", detail.SettingDetailINAM,
                        detail.SettingDetailOUTAM,
                        detail.SettingDetailINPM,
                        detail.SettingDetailOUTPM);

                }
                sSetting = setting;
                dtrTemp01.EmployeeNo = enrollee.EmployeeIdNo;
                lDT.Add(dtrTemp01);
            }
            return lDT;
        }

        #endregion

        public static List<DTR> LoadEnrolleeAttendanceDtrAll(List<Empoyee> lEnrollee, out List<MacDumpLog> lMacDumpLogs, string sUserName)
        {
            var listDTR = new List<DTR>();
            var listMacDump = new List<MacDumpLog>();
            var listMc = new List<MacDumpLog>();
            foreach (var enrollee in lEnrollee)
            {
                //Load logs for the particular user
                var listLogs = MachineManager.GetAll(enrollee.EmployeeNo);
                var tempListDTR = LoadEnrolleeAttendanceDTR(enrollee, listLogs.FindAll(l => (l.EnrolleeNo == enrollee.EmployeeNo)), out listMacDump, sUserName);
                listDTR.AddRange(tempListDTR);
                listMc.AddRange(listMacDump);
            }

            lMacDumpLogs = listMc;
            return listDTR;
        }

        public static bool IsHoliday(DateTime dt, out string holiday)
        {
            var bResult = false;
            var sHday = string.Empty;
            using (var data = new BioModel())
            {
                var h = data.Holidays.FirstOrDefault(ho => ho.HolidayDate == dt);
                if (h != null)
                {
                    bResult = true;
                    sHday = h.HolidayType;
                }
            }
            holiday = sHday;
            return bResult;
        }
        public static Dictionary<DateTime, string> LoadLeaveDictionary(int iEnrolleeId, int iMonth, int iYear)
        {
            var dLeave = new Dictionary<DateTime, string>();

            using (var data = new BioModel() )
            {
                var q = from leave in data.Leaves
                        where (leave.EmployeeId == iEnrolleeId)
                        select leave;

                var listLeave = q.ToList();

                foreach (var item in listLeave)
                {
                    //Leave l = listLeave.FirstOrDefault(le =>
                    //        ((le.EnrolleeId == iEnrolleeId) && (le.LeaveDateFrom.Value.Month == iMonth) &&
                    //         (le.LeaveDateFrom.Value.Year == iYear)));
                    if (item.LeaveDateFrom?.Month != iMonth || (item.LeaveDateFrom.Value.Year != iYear)) continue;
                    for (var i = 0; i < item.LeaveNoDays; i++)
                    {
                        var tempDt = Convert.ToDateTime(item.LeaveDateFrom);
                        var tempDateTime = tempDt.AddDays(i);
                        var tempLeave = item.LeaveType.LeaveTypeName;
                        try
                        {
                            dLeave.Add(tempDateTime, tempLeave);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(@"Error - " + ex.Message);
                            //throw new Exception("Duplicate exist."); 
                        }
                    }
                }
            }
            return dLeave;
        }

        public List<Machine> RemoveDuplicate(IEnumerable<Machine> lLogs)
        {
            var lMacDb = MachineManager.GetAll();
            var listMacDB = lMacDb.Select(item => new DatData
            {
                Pin = item.EnrolleeNo.ToString(), AttTime = String.Format(@"{0}-{1}-{2} {3}:{4}:{5}", item.IYear, item.IMonth, item.IDay, item.IHour, item.IMin, item.ISec)
            }).ToList();

            var listMacDBTemp = lLogs.Select(item => new DatData
            {
                AttTime = String.Format(@"{0}-{1}-{2} {3}:{4}:{5}", item.IYear, item.IMonth, item.IDay, item.IHour, item.IMin, item.ISec)
            }).ToList();


            var listMachine = (from log in listMacDBTemp
                let mac = listMacDB.FirstOrDefault(ma => ma.AttTime == log.AttTime)
                where mac == null
                select new Machine
                {
                    IYear = Convert.ToDateTime(log.AttTime).Year, IMonth = Convert.ToDateTime(log.AttTime).Month, IDay = Convert.ToDateTime(log.AttTime).Day, IHour = Convert.ToDateTime(log.AttTime).Hour, IMin = Convert.ToDateTime(log.AttTime).Minute, ISec = Convert.ToDateTime(log.AttTime).Second
                }).ToList();

            return listMachine;
        }

        public static List<Machine> RemoveDuplicateUsb(List<DatData> listLogs, int instanceId)
        {
            var lMacDB = MachineManager.GetAll();
            var listMacDB = lMacDB.Select(item => new DatData
            {
                Pin = item.EnrolleeNo.Trim(),
                AttTime = String.Format(@"{0}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}", item.IYear, item.IMonth, item.IDay, item.IHour, item.IMin, item.ISec)
            }).ToList();


            //var listMachine = (from log in listLogs
            //                   let mac = listMacDB.FirstOrDefault(ma => ma.AttTime == log.AttTime)
            //                   where mac == null
            //                   select new Machine
            //                   {
            //                       MachineInsId = instanceId,
            //                       EnrolleeNo = log.Pin.Trim(),
            //                       IYear = Convert.ToDateTime(log.AttTime).Year,
            //                       IMonth = Convert.ToDateTime(log.AttTime).Month,
            //                       IDay = Convert.ToDateTime(log.AttTime).Day,
            //                       IHour = Convert.ToDateTime(log.AttTime).Hour,
            //                       IMin = Convert.ToDateTime(log.AttTime).Minute,
            //                       ISec = Convert.ToDateTime(log.AttTime).Second
            //                   }).ToList();

            return (from log in listLogs
                let result = listMacDB.FirstOrDefault(f => f.AttTime == log.AttTime)
                where result == null
                select new Machine
                {
                    MachineInsId = instanceId, EnrolleeNo = log.Pin.Trim(), IYear = Convert.ToDateTime(log.AttTime).Year, IMonth = Convert.ToDateTime(log.AttTime).Month, IDay = Convert.ToDateTime(log.AttTime).Day, IHour = Convert.ToDateTime(log.AttTime).Hour, IMin = Convert.ToDateTime(log.AttTime).Minute, ISec = Convert.ToDateTime(log.AttTime).Second
                }).ToList();
        }

        public static List<DTR> LoadEnrolleeAttendanceDTR(Empoyee eEnrollee, List<Machine> lAttLog, out List<MacDumpLog> lDumpLogs, string sUserName)
        {
            var lAttendance = new List<DTR>();
            var lMacDumpLog = new List<MacDumpLog>();
            foreach (var log in lAttLog)
            {
                MacDumpLog oMacDump = new MacDumpLog();
                var dtTime = string.Format("{0:00}:{1:00}", log.IHour, log.IMin); //+ ":" + log.ISecond;
                var dtDate = string.Format("{0:}/{1:00}/{2:00}", log.IYear, log.IMonth, log.IDay);

                //var dtr = lAttendance.FirstOrDefault(at => at.DTRDate == DateTime.Parse(dtDate));
                var dtr = lAttendance.FirstOrDefault(at => at.DTRDate == DateTime.Parse(dtDate));

                var iLoc = 0;
                if (dtr != null)
                {
                    dtr.DTRDate = DateTime.Parse(dtDate);
                    dtr.DTRDay = DataTimeClass.VerifyDay(DateTime.Parse(dtDate));
                    dtr.EmployeeNo = eEnrollee.EmployeeNo;
                    dtr.EmployeeId = eEnrollee.EmployeeId;
                    dtr.IsSource = false;
                    dtr.EditedBy = sUserName;
                    dtr.EditedOn = DateTime.Now;

                    iLoc = VerifyShiftSettingViaList(eEnrollee.EmployeeNo, DateTime.Parse(dtDate), DateTime.Parse(dtTime), SettingDetailManager.GetAll(eEnrollee.SettingId));
                    switch (iLoc)
                    {
                        case 1:
                            if (dtr.TimeInAM != null)
                            {
                                oMacDump = new MacDumpLog();
                                //To Delinquent table
                                Console.WriteLine(dtTime);
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime =  DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeInAM = DataTimeClass.Convert24To12Format(dtTime);
                            }
                            break;
                        case 2:
                            if (dtr.TimeOutAM != null)
                            {
                                //To Delinquent table
                                oMacDump = new MacDumpLog();
                                Console.WriteLine(dtTime.ToString());
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime = DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeOutAM = DataTimeClass.Convert24To12Format(dtTime); ;
                            }

                            break;
                        case 3:
                            if (dtr.TimeInPM != null)
                            {
                                //To Delinquent table
                                oMacDump = new MacDumpLog();
                                Console.WriteLine(dtTime.ToString());
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime = DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeInPM = DataTimeClass.Convert24To12Format(dtTime); ;
                            }

                            break;
                        case 4:
                            if (dtr.TimeOutPM != null)
                            {
                                //To Delinquent table
                                oMacDump = new MacDumpLog();
                                Console.WriteLine(dtTime);
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime = DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeOutPM = DataTimeClass.Convert24To12Format(dtTime); ;
                            }

                            break;
                        case 5:
                            if (dtr.TimeInOT != null)
                            {
                                //To Delinquent table
                                oMacDump = new MacDumpLog();
                                Console.WriteLine(dtTime);
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime = DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeInOT = DataTimeClass.Convert24To12Format(dtTime); ;
                            }


                            break;
                        case 6:
                            if (dtr.TimeOutOT != null)
                            {
                                //To Delinquent table
                                oMacDump = new MacDumpLog();
                                Console.WriteLine(DateTime.Parse(dtTime).ToString());
                                oMacDump.MacDumpDate = dtDate;
                                oMacDump.MacDumpTime = DataTimeClass.Convert24To12Format(dtTime);

                                oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                                oMacDump.EmployeeId = eEnrollee.EmployeeId;
                                oMacDump.MachineNo = log.MachineNo;
                            }
                            else
                            {
                                dtr.TimeOutOT = DataTimeClass.Convert24To12Format(dtTime); ;
                            }

                            break;
                        case 7:

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    dtr = new DTR();
                    //iCountAtt = lAttLog.Count<AttLog>(attl => attl.GetDate() == dtDate);
                    dtr.DTRDate = DateTime.Parse(dtDate);
                    dtr.DTRDay = DataTimeClass.VerifyDay(DateTime.Parse(dtDate));
                    dtr.EmployeeNo = eEnrollee.EmployeeNo;
                    dtr.EmployeeId = eEnrollee.EmployeeId;
                    dtr.IsSource = false;
                    dtr.EditedBy = sUserName;
                    dtr.EditedOn = DateTime.Now;

                    oMacDump.EmployeeNo = eEnrollee.EmployeeNo;
                    oMacDump.EmployeeId = eEnrollee.EmployeeId;

                    iLoc = VerifyShiftSettingViaList(eEnrollee.EmployeeNo, DateTime.Parse(dtDate), DateTime.Parse(dtTime), SettingDetailManager.GetAll(eEnrollee.SettingId));
                    switch (iLoc)
                    {
                        case 1:
                            dtr.TimeInAM = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 2:
                            dtr.TimeOutAM = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 3:
                            dtr.TimeInPM = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 4:
                            dtr.TimeOutPM = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 5:
                            dtr.TimeInOT = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 6:
                            dtr.TimeOutOT = DataTimeClass.Convert24To12Format(dtTime); ;
                            break;
                        case 7:

                            break;
                        default:
                            break;
                    }
                    if (!IsDTRExisting(dtr))
                        lAttendance.Add(dtr);
                }
                if (oMacDump == null) continue;
                if (!IsMacDumpExisting(oMacDump))
                    lMacDumpLog.Add(oMacDump);
            }
            lDumpLogs = lMacDumpLog;
            return lAttendance;
        }

        public static int VerifyShiftSettingViaList(string sEnrollNumber,
            DateTime dtDate, DateTime dtTime, List<SettingDetail> lSettings)
        {
            int iSet = 0;
            foreach (var settings in lSettings)
            {
                if (settings.SettingDetailActive == true)
                {
                    if (settings.SettingDetailDay.Trim() == DataTimeClass.VerifyDay(dtDate))
                    {

                        if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailAMIn01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailAMIn01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailAMIn01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailAMIn02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailAMIn02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailAMIn02).Minute))))
                        {
                            iSet = 1;
                        }
                        else if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailAMOut01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailAMOut01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailAMOut01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailAMOut02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailAMOut02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailAMOut02).Minute))))
                        {
                            iSet = 2;
                        }
                        else if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailPMIn01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailPMIn01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailPMIn01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailPMIn02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailPMIn02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailPMIn02).Minute))))
                        {
                            iSet = 3;
                        }
                        else if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailPMOut01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailPMOut01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailPMOut01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailPMOut02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailPMOut02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailPMOut02).Minute))))
                        {
                            iSet = 4;
                        }
                        else if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailOTIn01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailOTIn01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailOTIn01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailOTin02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailOTin02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailOTin02).Minute))))
                        {
                            iSet = 5;
                        }
                        else if (((dtTime.Hour > Convert.ToDateTime(settings.SettingDetailOTOut01).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailOTOut01).Hour) && (dtTime.Minute >= Convert.ToDateTime(settings.SettingDetailOTOut01).Minute))) &&
                            ((dtTime.Hour < Convert.ToDateTime(settings.SettingDetailOTOut02).Hour) || ((dtTime.Hour == Convert.ToDateTime(settings.SettingDetailOTOut02).Hour) && (dtTime.Minute <= Convert.ToDateTime(settings.SettingDetailOTOut02).Minute))))
                        {
                            iSet = 6;
                        }
                        else
                        {
                            iSet = 7;
                        }
                    }
                }
            }
            return iSet;
        }

        public static bool IsDTRExisting(DTR dtr)
        {
            var bResult = false;
            using (var data = new BioModel())
            {
                bool bFlag = false;
                DTR d = data.DTRs.FirstOrDefault(dt => ((dtr.EmployeeNo == dt.EmployeeNo) && (dtr.DTRDate == dt.DTRDate)));
                if (d != null)
                {
                    if (dtr.TimeInAM != null)
                    {
                        if ((d.TimeInAM == null))
                        {
                            d.TimeInAM = dtr.TimeInAM;
                            bFlag = true;
                        }
                        else if (d.TimeInAM.Trim().Length == 0)
                        {
                            d.TimeInAM = dtr.TimeInAM;
                            bFlag = true;
                        }
                    }
                    else if (dtr.TimeOutAM != null)
                    {
                        if ((d.TimeOutAM == null))
                        {
                            d.TimeOutAM = dtr.TimeOutAM;
                            bFlag = true;
                        }
                        else if ((d.TimeOutAM.Trim().Length == 0))
                        {
                            d.TimeOutAM = dtr.TimeOutAM;
                            bFlag = true;
                        }
                    }
                    else if (dtr.TimeInPM != null)
                    {
                        if ((d.TimeInPM == null))
                        {
                            d.TimeInPM = dtr.TimeInPM;
                            bFlag = true;
                        }
                        else if ((d.TimeInPM.Trim().Length == 0))
                        {
                            d.TimeInPM = dtr.TimeInPM;
                            bFlag = true;
                        }
                    }
                    else if (dtr.TimeOutPM != null)
                    {
                        if ((d.TimeOutPM == null))
                        {
                            bFlag = true;
                            d.TimeOutPM = dtr.TimeOutPM;
                        }
                        else if ((d.TimeOutPM.Trim().Length == 0))
                        {
                            bFlag = true;
                            d.TimeOutPM = dtr.TimeOutPM;
                        }
                    }
                    else if (dtr.TimeInOT != null)
                    {
                        if ((d.TimeInOT == null))
                        {
                            bFlag = true;
                            d.TimeInOT = dtr.TimeInOT;
                        }
                        else if ((d.TimeInOT.Trim().Length == 0))
                        {
                            bFlag = true;
                            d.TimeInOT = dtr.TimeInOT;
                        }
                    }
                    else if (dtr.TimeOutOT != null)
                    {
                        if ((d.TimeOutOT == null))
                        {
                            bFlag = true;
                            d.TimeOutOT = dtr.TimeOutOT;
                        }
                        else if ((d.TimeOutOT.Trim().Length == 0))
                        {
                            bFlag = true;
                            d.TimeOutOT = dtr.TimeOutOT;
                        }
                    }

                    if (bFlag == true)
                        DTRManager.Save(d);

                    bResult = true;
                }
            }
            return bResult;
        }

        public static bool IsMacDumpExisting(MacDumpLog macDumpLog)
        {
            var bResult = false;
            using (var data = new BioModel())
            {
                MacDumpLog m = data.MacDumpLogs.FirstOrDefault(ma => ((macDumpLog.EmployeeNo == ma.EmployeeNo) && (macDumpLog.MacDumpDate == ma.MacDumpDate)));
                if (m != null)
                {
                    if (m.MacDumpTime != macDumpLog.MacDumpTime)
                    {
                        m.MacDumpTime = macDumpLog.MacDumpTime;

                        data.SaveChanges();
                    }
                    bResult = true;
                }
            }
            return bResult;
        }

        public static List<DtrData> LoadDTRViaDTRBatch(Empoyee enrollee, int iMonth, int iStart, int iEnd, int iYear, List<DTR> lDTRs, string sName, string sDuration)
        {
            var lDT = new List<DtrData>();
            var lSetDeatils = new List<SettingDetail>();
            var dLeave = LoadLeaveDictionary(enrollee.EmployeeId, iMonth, iYear);
            //var Misc = oMisc;
            var graceInAm = ConfigurationManager.AppSettings["GraceIn-AM"];
            var graceInPm = ConfigurationManager.AppSettings["GraceIn-PM"];
            var graceInOt = ConfigurationManager.AppSettings["GraceIn-OT"];

            var regular = ConfigurationManager.AppSettings["RegularLabel"];
            var undertime = ConfigurationManager.AppSettings["UndertimeLabel"];
            var overtime = ConfigurationManager.AppSettings["OvertimrLabel"];

            lSetDeatils = SettingDetailManager.GetAll((Convert.ToInt16(enrollee.SettingId)));
            int iDaysMonth = DataTimeClass.GetDaysInMonth(iYear, iMonth);
            for (int i = 1; i <= iDaysMonth; i++)
            {
                DtrData dtrTemp01 = new DtrData();
                DateTime dtTemp = Convert.ToDateTime(iMonth.ToString() + "/" + i.ToString() + "/" + iYear.ToString());
                DTR dtrTemp = lDTRs.FirstOrDefault(d => d.DTRDate == dtTemp);
                string sDay = DataTimeClass.VerifyDay(dtTemp);
                string sHolidayTemp = string.Empty;
                TimeSpan? iTimeAM = new TimeSpan();
                TimeSpan? iTimePM = new TimeSpan();
                TimeSpan? iTimeOT = new TimeSpan();
                double? iTotalTime;

                if ((i >= iStart) && (i <= iEnd))
                {
                    if (dtrTemp != null)
                    {
                        double? iTotalTimeAM = 0;
                        double? iTotalTimePM = 0;
                        double? iTotalTimeOT = 0;

                        dtrTemp01.DTRDate = dtrTemp.DTRDate;
                        dtrTemp01.DTRDay = dtrTemp.DTRDay;
                        dtrTemp01.DTRId = dtrTemp.DTRId;
                        dtrTemp01.DTRStatus = dtrTemp.DTRStatus;
                        dtrTemp01.TimeInAM = dtrTemp.TimeInAM;
                        dtrTemp01.TimeInOT = dtrTemp.TimeInOT;
                        dtrTemp01.TimeInPM = dtrTemp.TimeInPM;
                        dtrTemp01.TimeOutAM = dtrTemp.TimeOutAM;
                        dtrTemp01.TimeOutOT = dtrTemp.TimeOutOT;
                        dtrTemp01.TimeOutPM = dtrTemp.TimeOutPM;
                        dtrTemp01.TotalHour = dtrTemp.TotalHour;
                        dtrTemp01.TotalHours = dtrTemp.TotalHours;
                        dtrTemp01.TotalMinute = dtrTemp.TotalMinute;

                        foreach (var detail in lSetDeatils)
                        {
                            if (detail.SettingDetailDay.Trim() == sDay.Trim())
                            {
                                DateTime? dtTimeInAM = null;
                                DateTime? dtTimeOutAM = null;
                                DateTime? dtTimeInPM = null;
                                DateTime? dtTimeOutPM = null;
                                DateTime? dtTimeInOT = null;
                                DateTime? dtTimeOutOT = null;

                                double dOver = 0;

                                if (dtrTemp.TimeInAM != null)
                                {
                                    DateTime tempTimeInAM;
                                    if (DateTime.TryParse(dtrTemp.TimeInAM, out tempTimeInAM))
                                    {
                                        //dtTimeInAM = Convert.ToDateTime(dtrTemp.TimeInAM);
                                        dtTimeInAM = tempTimeInAM;
                                        //if (Misc.MiscActive == true)
                                        //{
                                            string shortTimeString = detail.SettingDetailINAM.Value.ToShortTimeString();
                                            DateTime tempSetTime = Convert.ToDateTime(shortTimeString);

                                            //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                            if (DataTimeClass.IsCompareTime((DateTime)dtTimeInAM, tempSetTime, 0,
                                                                            Convert.ToDouble(graceInAm),
                                                                            out dOver))
                                            {
                                                DateTime tempTime = dOver > 0
                                                                        ? dtTimeInAM.Value.AddMinutes(
                                                                            -Convert.ToDouble(graceInAm))
                                                                        : tempSetTime;
                                                //DateTime tempTime =
                                                //    dtTimeInAM.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInAM));

                                                //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                                //{
                                                //dtTimeInAM = (Misc.MiscGraceInAM <= tempTime.Minute)
                                                //                 ? tempTime
                                                //                 : tempSetTime;

                                                dtTimeInAM = (tempTime.Minute > 0)
                                                                 ? tempTime
                                                                 : tempSetTime;
                                                //}
                                            }
                                            else
                                            {
                                                dtTimeInAM = tempSetTime;
                                            }
                                        }
                                        //else
                                        //{
                                        //    string shortTimeString = detail.SettingDetailINAM.Value.ToShortTimeString();
                                        //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                        //    dtTimeInAM = DataTimeClass.IsCompareTime(dtTimeInAM.Value, tempSetTime, 0,
                                        //                                             Convert.ToDouble(Misc.MiscGraceInAM),
                                        //                                             out dOver)
                                        //                     ? dtTimeInAM
                                        //                     : tempSetTime;
                                        //}
                                    //}
                                    else
                                    {
                                        dtTimeInAM = null;
                                    }
                                }

                                if (dtrTemp.TimeOutAM != null)
                                {
                                    DateTime tempTimeOutAM;
                                    if (DateTime.TryParse(dtrTemp.TimeOutAM, out tempTimeOutAM))
                                    {
                                        //dtTimeOutAM = Convert.ToDateTime(dtrTemp.TimeOutAM);
                                        dtTimeOutAM = tempTimeOutAM;
                                        string shortTimeString = detail.SettingDetailOUTAM.Value.ToShortTimeString();
                                        DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                        dtTimeOutAM = DataTimeClass.IsCompareTime((DateTime)dtTimeOutAM, dtShort, 1, 0,
                                                                                  out dOver)
                                                          ? dtTimeOutAM
                                                          : dtShort;
                                    }
                                }

                                if (dtrTemp.TimeInPM != null)
                                {
                                    DateTime tempTimeInPM;
                                    if (DateTime.TryParse(dtrTemp.TimeInPM, out tempTimeInPM))
                                    {
                                        //dtTimeInPM = Convert.ToDateTime(dtrTemp.TimeInPM);
                                        dtTimeInPM = tempTimeInPM;
                                        //if (Misc.MiscActive == true)
                                        //{
                                            string shortTimeString = detail.SettingDetailINPM.Value.ToShortTimeString();
                                            DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                            //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                            if (DataTimeClass.IsCompareTime((DateTime)dtTimeInPM, tempSetTime, 0,
                                                                            Convert.ToDouble(graceInPm),
                                                                            out dOver))
                                            {

                                                //this is the culprit --------->>>>
                                                //DateTime tempTime =
                                                //    dtTimeInPM.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInPM));
                                                DateTime tempTime = dOver > 0
                                                                        ? dtTimeInPM.Value.AddMinutes(
                                                                            -Convert.ToDouble(graceInPm))
                                                                        : tempSetTime;
                                                //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                                //{
                                                //dtTimeInPM = (Misc.MiscGraceInPM <= tempTime.Minute)
                                                //                 ? tempTime
                                                //                 : tempSetTime;

                                                dtTimeInPM = (tempTime.Minute > 0)
                                                                 ? tempTime
                                                                 : tempSetTime;
                                                //}
                                            }
                                            else
                                            {
                                                dtTimeInPM = tempSetTime;
                                            }
                                        //}
                                        //else
                                        //{
                                        //    string shortTimeString = detail.SettingDetailINPM.Value.ToShortTimeString();
                                        //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                        //    dtTimeInPM = DataTimeClass.IsCompareTime(dtTimeInPM.Value, tempSetTime, 0,
                                        //                                             Convert.ToDouble(Misc.MiscGraceInPM),
                                        //                                             out dOver)
                                        //                     ? dtTimeInPM
                                        //                     : tempSetTime;
                                        //}
                                    }
                                    else
                                    {
                                        dtTimeInPM = null;
                                    }
                                }
                                if (dtrTemp.TimeOutPM != null)
                                {
                                    DateTime tempTimeOutPM;
                                    if (DateTime.TryParse(dtrTemp.TimeOutPM, out tempTimeOutPM))
                                    {
                                        //dtTimeOutPM = Convert.ToDateTime(dtrTemp.TimeOutPM);
                                        dtTimeOutPM = tempTimeOutPM;
                                        string shortTimeString = detail.SettingDetailOUTPM.Value.ToShortTimeString();
                                        DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                        dtTimeOutPM = DataTimeClass.IsCompareTime((DateTime)dtTimeOutPM, dtShort, 1, 0,
                                                                                  out dOver)
                                                          ? dtTimeOutPM
                                                          : dtShort;
                                    }
                                }

                                if (dtrTemp.TimeInOT != null)
                                {
                                    DateTime tempTimeInOT;
                                    if (DateTime.TryParse(dtrTemp.TimeInOT, out tempTimeInOT))
                                    {
                                        //dtTimeInOT = Convert.ToDateTime(dtrTemp.TimeInOT);
                                        dtTimeInOT = tempTimeInOT;
                                        //if (Misc.MiscActive == true)
                                        //{
                                            string shortTimeString = detail.SettingDetailINOT.Value.ToShortTimeString();
                                            DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                            //dtTimeInAM = DateTimeClass.IsCompareTime(dtTimeInAM, Convert.ToDateTime(detail.SettingDetailINAM), 0) ? dtTimeInAM : Convert.ToDateTime(detail.SettingDetailINAM);
                                            if (DataTimeClass.IsCompareTime((DateTime)dtTimeInOT, tempSetTime, 0,
                                                                            Convert.ToDouble(graceInOt),
                                                                            out dOver))
                                            {
                                                //DateTime tempTime =
                                                //    dtTimeInOT.Value.AddMinutes(-Convert.ToDouble(Misc.MiscGraceInOT));
                                                DateTime tempTime = dOver > 0
                                                                        ? dtTimeInOT.Value.AddMinutes(
                                                                            -Convert.ToDouble(graceInOt))
                                                                        : dtTimeInOT.Value;
                                                //if (DateTimeClass.IsCompareTime(tempTime, tempSetTime, 0))
                                                //{
                                                dtTimeInOT = (Convert.ToInt16(graceInOt) <= tempTime.Minute)
                                                                 ? tempTime
                                                                 : tempSetTime;
                                                //}
                                            }
                                            else
                                            {
                                                dtTimeInOT = tempSetTime;
                                            }
                                        //}
                                        //else
                                        //{
                                        //    string shortTimeString = detail.SettingDetailINOT.Value.ToShortTimeString();
                                        //    DateTime tempSetTime = Convert.ToDateTime(shortTimeString);
                                        //    dtTimeInOT = DataTimeClass.IsCompareTime(dtTimeInOT.Value, tempSetTime, 0,
                                        //                                             Convert.ToDouble(graceInOt),
                                        //                                             out dOver)
                                        //                     ? dtTimeInPM
                                        //                     : tempSetTime;
                                        //}
                                    }
                                    else
                                    {
                                        dtTimeInOT = null;
                                    }
                                }
                                if (dtrTemp.TimeOutOT != null)
                                {
                                    DateTime tempTimeOutOT;
                                    if (DateTime.TryParse(dtrTemp.TimeOutOT, out tempTimeOutOT))
                                    {
                                        //dtTimeOutOT = Convert.ToDateTime(dtrTemp.TimeOutOT);
                                        dtTimeOutOT = tempTimeOutOT;
                                        string shortTimeString = detail.SettingDetailOUTPM.Value.ToShortTimeString();
                                        DateTime dtShort = Convert.ToDateTime(shortTimeString);
                                        dtTimeOutOT = DataTimeClass.IsCompareTime((DateTime)dtTimeOutOT, dtShort, 1, 0,
                                                                                  out dOver)
                                                          ? dtTimeOutOT
                                                          : dtShort;
                                    }
                                }

                                DateTime? tempDtInAM = null;
                                if (dtTimeInAM != null)
                                    tempDtInAM = Convert.ToDateTime(dtTimeInAM.Value.ToString("HH:mm"));

                                DateTime? tempDtOutAM = null;
                                if (dtTimeOutAM != null)
                                    tempDtOutAM = Convert.ToDateTime(dtTimeOutAM.Value.ToString("HH:mm"));

                                DateTime? tempDtInOT = null;
                                if (dtTimeInOT != null)
                                    Convert.ToDateTime(dtTimeInOT.Value.ToString("HH:mm"));

                                DateTime? tempDtOutOT = null;
                                if (dtTimeOutOT != null)
                                    Convert.ToDateTime(dtTimeOutOT.Value.ToString("HH:mm"));

                                iTimeAM = tempDtOutAM - tempDtInAM; // dtTimeOutAM.Subtract(dtTimeInAM);
                                iTimePM = dtTimeOutPM - dtTimeInPM;
                                iTimeOT = tempDtOutOT - tempDtInOT;
                            }
                        }

                        double? iHourAM = null;
                        if (iTimeAM != null)
                            iHourAM = iTimeAM.Value.Hours;

                        double? iMinAM = null;
                        if (iTimeAM != null)
                            iMinAM = iTimeAM.Value.Minutes;

                        iTotalTimeAM = (iHourAM + (iMinAM / 60));

                        double? iHourPM = null;
                        if (iTimePM != null)
                            iHourPM = iTimePM.Value.Hours;

                        double? iMinPM = null;
                        if (iTimePM != null)
                            iMinPM = iTimePM.Value.Minutes;

                        iTotalTimePM = (iHourPM + (iMinPM / 60));

                        double? iHourOT = 0;
                        if (iTimeOT != null)
                            iHourOT = iTimeOT.Value.Hours;

                        double? iMinOT = 0;
                        if (iMinOT != 0)
                            iMinOT = iTimeOT.Value.Minutes;

                        iTotalTimeOT = (iHourOT + (iMinOT / 60));

                        //if (Misc.MiscActive == true)
                        //{
                            double dHour = 8;
                            iTotalTime = iTotalTimeAM + iTotalTimePM + iTotalTimeOT;

                            dtrTemp01.TotalHour = iTotalTime;

                            //Computer for hour
                            dtrTemp01.TotalHour = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                  (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                      ? ((iHourAM + iHourPM))
                                                      : 0;

                            dtrTemp01.TotalHour = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                      ? ((dtrTemp01.TotalHour + iHourOT))
                                                      : (dtrTemp01.TotalHour);


                            //Compute for minutes
                            dtrTemp01.TotalMinute = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                    (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                        ? (iMinAM + iMinPM)
                                                        : 0;

                            dtrTemp01.TotalMinute = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                        ? (dtrTemp01.TotalMinute + iMinOT)
                                                        : dtrTemp01.TotalMinute;


                            if (dtrTemp01.TotalMinute > 0)
                            {
                                if (dtrTemp01.TotalMinute >= 60)
                                {
                                    int tempToHour = Convert.ToInt32(Convert.ToDouble(dtrTemp01.TotalMinute) / 60);
                                    int tempToMin = Convert.ToInt32((Convert.ToDouble(dtrTemp01.TotalMinute) % 60));
                                    dtrTemp01.TotalHour = (dtrTemp01.TotalHour + tempToHour) - dHour;
                                    dtrTemp01.TotalMinute = 60 - tempToMin;
                                }
                                else
                                {
                                    dtrTemp01.TotalHour = (dtrTemp01.TotalHour + 1) - dHour;
                                    double? dMinTotal = 60 - dtrTemp01.TotalMinute;
                                    dtrTemp01.TotalMinute = dMinTotal;
                                }
                            }

                            //Compute for total hours
                            double? tempITotaltime = iTotalTime - dHour;
                            dtrTemp01.TotalHours = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                   (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                       ? tempITotaltime
                                                       : 0;
                            dtrTemp01.TotalHours = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null)
                                                       ? (dtrTemp01.TotalHours + iTotalTimeOT)
                                                       : dtrTemp01.TotalHours;

                            if (dtrTemp01.TotalHours == 0)
                            {
                                //dtrTemp01.DTRStatus = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                //                      (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                //                       ? Misc.MiscRegularLabel
                                //                       : "-";
                                dtrTemp01.DTRStatus = (dtrTemp.TimeInAM != null) && (dtrTemp.TimeOutAM != null) &&
                                                      (dtrTemp.TimeInPM != null) && (dtrTemp.TimeOutPM != null)
                                                          ? regular
                                                          : "-";
                                dtrTemp01.TotalHour = 0;
                            }
                            else if (dtrTemp01.TotalHours > 0)
                            {
                                dtrTemp01.DTRStatus = overtime;
                                //dtrTemp01.TotalHour = dtrTemp01.TotalHours;
                                //dtrTemp01.TotalMinute = 0;
                                dtrTemp01.TotalHour = null;
                                dtrTemp01.TotalMinute = null;
                                dtrTemp01.TotalHours = null;
                            }
                            else if (dtrTemp01.TotalHours < 0)
                            {
                                dtrTemp01.DTRStatus = undertime;
                                //dtrTemp01.TotalMinute = dtrTemp01.TotalHours;
                                //dtrTemp01.TotalHour = 0;
                            }
                            else
                            {
                                dtrTemp01.DTRStatus = "-";
                                //dtrTemp01.TotalHour = 0;
                                //dtrTemp01.TotalMinute = 0;
                            }
                        //}
                        //else
                        //{
                        //    iTotalTime = iTotalTimeAM + iTotalTimePM + iTotalTimeOT;
                        //    dtrTemp01.TotalHours = iTotalTime;
                        //}

                        if (IsHoliday(dtTemp, out sHolidayTemp))
                            dtrTemp01.DTRStatus = "(h)" + sHolidayTemp;

                        if (dLeave.ContainsKey(dtTemp))
                        {
                            var tempStatus = dLeave[dtTemp];
                            //A.M.
                            if (dtrTemp01.TimeInAM == null)
                                dtrTemp01.TimeInAM = tempStatus;
                            else if (dtrTemp01.TimeInAM.Trim().Length == 0)
                                dtrTemp01.TimeInAM = tempStatus;

                            if (dtrTemp01.TimeOutAM == null)
                                dtrTemp01.TimeOutAM = tempStatus;
                            else if (dtrTemp01.TimeOutAM.Trim().Length == 0)
                                dtrTemp01.TimeOutAM = tempStatus;

                            //P.M.
                            if (dtrTemp01.TimeInPM == null)
                                dtrTemp01.TimeInPM = tempStatus;
                            else if (dtrTemp01.TimeInPM.Trim().Length == 0)
                                dtrTemp01.TimeInPM = tempStatus;

                            if (dtrTemp01.TimeOutPM == null)
                                dtrTemp01.TimeOutPM = tempStatus;
                            else if (dtrTemp01.TimeOutPM.Trim().Length == 0)
                                dtrTemp01.TimeOutPM = tempStatus;

                            ////if (dtrTemp01.TimeInAM == null)
                            ////    dtrTemp01.TimeInAM = "L/O.B.";
                            ////else if (dtrTemp01.TimeInAM.Trim().Length == 0)
                            ////    dtrTemp01.TimeInAM = "L/O.B.";

                            ////if (dtrTemp01.TimeOutAM == null)
                            ////    dtrTemp01.TimeOutAM = "L/O.B.";
                            ////else if (dtrTemp01.TimeOutAM.Trim().Length == 0)
                            ////    dtrTemp01.TimeOutAM = "L/O.B";

                            //////P.M.
                            ////if (dtrTemp01.TimeInPM == null)
                            ////    dtrTemp01.TimeInPM = "L/O.B.";
                            ////else if (dtrTemp01.TimeInPM.Trim().Length == 0)
                            ////    dtrTemp01.TimeInPM = "L/O.B";

                            ////if (dtrTemp01.TimeOutPM == null)
                            ////    dtrTemp01.TimeOutPM = "L/O.B.";
                            ////else if (dtrTemp01.TimeOutPM.Trim().Length == 0)
                            ////    dtrTemp01.TimeOutPM = "L/O.B";

                            //O.T.
                            //if (dtrTemp01.TimeInOT == null)
                            //    dtrTemp01.TimeInOT = "L/O.B.";
                            //else if (dtrTemp01.TimeInOT.Trim().Length == 0)
                            //    dtrTemp01.TimeInOT = "L/O.B";

                            //if (dtrTemp01.TimeOutOT == null)
                            //    dtrTemp01.TimeOutOT = "L/O.B.";
                            //else if (dtrTemp01.TimeOutOT.Trim().Length == 0)
                            //    dtrTemp01.TimeOutOT = "L/O.B";

                            dtrTemp01.DTRStatus = dLeave[dtTemp];
                        }
                    }
                    else
                    {
                        if ((sDay == "Sunday") || (sDay == "Saturday"))
                        {
                            dtrTemp01.TimeInAM = sDay;
                            dtrTemp01.TimeOutAM = "-----";
                            dtrTemp01.TimeInPM = sDay;
                            dtrTemp01.TimeOutPM = "-----";
                            dtrTemp01.TimeInOT = sDay;
                            dtrTemp01.TimeOutOT = "-----";
                        }
                        //else
                        //{
                        //    dtrTemp01.TimeInAM = "-";
                        //    dtrTemp01.TimeOutAM = "-";
                        //    dtrTemp01.TimeInPM = "-";
                        //    dtrTemp01.TimeOutPM = "-";
                        //    dtrTemp01.TimeInOT = "-";
                        //    dtrTemp01.TimeOutOT = "-";
                        //}
                        dtrTemp01.DTRDate = dtTemp;
                        dtrTemp01.DTRDay = sDay;
                        //dtrTemp01.TotalHours = Convert.ToDouble(iTotalTime);

                        if (IsHoliday(dtTemp, out sHolidayTemp))
                        {
                            dtrTemp01.DTRStatus = sHolidayTemp + " (h)";
                            dtrTemp01.TimeInAM = "Holid";
                            dtrTemp01.TimeOutAM = "-----";
                            dtrTemp01.TimeInPM = "Holid";
                            dtrTemp01.TimeOutPM = "-----";
                            dtrTemp01.TimeInOT = "Holid";
                            dtrTemp01.TimeOutOT = "-----";
                        }

                        if (dLeave.ContainsKey(dtTemp))
                        {
                            //dtrTemp01.DTRStatus = dLeave[dtTemp];
                            //dtrTemp01.TimeInAM = "L/O.B.";
                            //dtrTemp01.TimeOutAM = "-----";
                            //dtrTemp01.TimeInPM = "L/O.B.";
                            //dtrTemp01.TimeOutPM = "-----";
                            //dtrTemp01.TimeInOT = "L/O.B.";
                            //dtrTemp01.TimeOutOT = "-----";

                            var tempStatus = dLeave[dtTemp];
                            dtrTemp01.DTRStatus = tempStatus;
                            dtrTemp01.TimeInAM = tempStatus;
                            dtrTemp01.TimeOutAM = "-----";
                            dtrTemp01.TimeInPM = tempStatus;
                            dtrTemp01.TimeOutPM = "-----";
                            dtrTemp01.TimeInOT = tempStatus;
                            dtrTemp01.TimeOutOT = "-----";
                        }
                    }
                }

                var setting = string.Empty;
                foreach (var detail in lSetDeatils)
                {
                    setting = string.Format(@"                                                     AM [{0:t}-{1:t}], PM [{2:t}-{3:t}]", detail.SettingDetailINAM,
                        detail.SettingDetailOUTAM,
                        detail.SettingDetailINPM,
                        detail.SettingDetailOUTPM);

                }

                dtrTemp01.Duration = sDuration;
                dtrTemp01.Settings = setting;
                dtrTemp01.EmployeeName = sName;
                lDT.Add(dtrTemp01);
            }
            return lDT;
        }
    }
}
