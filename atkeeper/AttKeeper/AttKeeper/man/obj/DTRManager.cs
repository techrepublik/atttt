using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class DTRManager
    {
        public static DataRepository<DTR> _d;

        public static int Save(DTR dtr)
        {
            var a = new DTR
            {
                DTRId = dtr.DTRId,
                EmployeeId = dtr.EmployeeId,
                EmployeeNo = dtr.EmployeeNo,
                DTRDate = dtr.DTRDate,
                DTRDay = dtr.DTRDay,
                TimeInAM = dtr.TimeInAM,
                TimeOutAM = dtr.TimeOutAM,
                TimeInPM = dtr.TimeInPM,
                TimeOutPM = dtr.TimeOutPM,
                TimeInOT = dtr.TimeInOT,
                TimeOutOT = dtr.TimeOutOT,
                TotalHours = dtr.TotalHours,
                TotalHour = dtr.TotalHour,
                TotalMinute = dtr.TotalMinute,
                DTRStatus = dtr.DTRStatus,
                IsSource = dtr.IsSource,
                EditedBy = dtr.EditedBy,
                EditedOn = dtr.EditedOn
            };

            using (_d = new DataRepository<DTR>())
            {
                if (dtr.DTRId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.DTRId;
        }
        public static int Save(List<DTR> listDTR)
        {
            var iResult = 0;
            try
            {
                using (_d = new DataRepository<DTR>())
                {
                    foreach (var a in listDTR.Select(dtr => new DTR
                    {
                        DTRId = dtr.DTRId,
                        EmployeeId = dtr.EmployeeId,
                        EmployeeNo = dtr.EmployeeNo,
                        DTRDate = dtr.DTRDate,
                        DTRDay = dtr.DTRDay,
                        TimeInAM = dtr.TimeInAM,
                        TimeOutAM = dtr.TimeOutAM,
                        TimeInPM = dtr.TimeInPM,
                        TimeOutPM = dtr.TimeOutPM,
                        TimeInOT = dtr.TimeInOT,
                        TimeOutOT = dtr.TimeOutOT,
                        TotalHours = dtr.TotalHours,
                        TotalHour = dtr.TotalHour,
                        TotalMinute = dtr.TotalMinute,
                        DTRStatus = dtr.DTRStatus,
                        IsSource = dtr.IsSource,
                        EditedBy = dtr.EditedBy,
                        EditedOn = dtr.EditedOn
                    }))
                    {
                        _d.Add(a);
                    }
                    _d.SaveChanges();
                }
                iResult = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(@"Error occured on {0}", ex.Message));
                iResult = 0;
            }

            return iResult;
        }

        public static int Save(List<DTR> listDTR, bool bUpdate)
        {
            var iResult = 0;
            try
            {
                using (_d = new DataRepository<DTR>())
                {
                    foreach (var a in listDTR.Select(dtr => new DTR
                    {
                        DTRId = dtr.DTRId,
                        EmployeeId = dtr.EmployeeId,
                        EmployeeNo = dtr.EmployeeNo,
                        DTRDate = dtr.DTRDate,
                        DTRDay = dtr.DTRDay,
                        TimeInAM = dtr.TimeInAM,
                        TimeOutAM = dtr.TimeOutAM,
                        TimeInPM = dtr.TimeInPM,
                        TimeOutPM = dtr.TimeOutPM,
                        TimeInOT = dtr.TimeInOT,
                        TimeOutOT = dtr.TimeOutOT,
                        TotalHours = dtr.TotalHours,
                        TotalHour = dtr.TotalHour,
                        TotalMinute = dtr.TotalMinute,
                        DTRStatus = dtr.DTRStatus,
                        IsSource = dtr.IsSource,
                        EditedBy = dtr.EditedBy,
                        EditedOn = dtr.EditedOn
                    }))
                    {
                        _d.Update(a);
                    }
                    _d.SaveChanges();
                }
                iResult = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(@"Error occured on {0}", ex.Message));
                iResult = 0;
            }

            return iResult;
        }

        public static bool Delete(DTR dtr)
        {
            using (_d = new DataRepository<DTR>())
            {
                _d.Delete(dtr);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<DTR>())
            {
                _d.Delete(d => d.DTRId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<DTR> GetAll(int? iId)
        {
            using (_d = new DataRepository<DTR>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.EmployeeId == iId).OrderBy(o => o.DTRDate).ToList();
            }
        }
    }
}
