using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class MacDumpLogManager
    {
        public static DataRepository<MacDumpLog> _d;

        public static int Save(MacDumpLog macDump)
        {
            var a = new MacDumpLog
            {
                MacDumpLogId = macDump.MacDumpLogId,
                EmployeeId = macDump.EmployeeId,
                MachineNo = macDump.MachineNo,
                EmployeeNo = macDump.EmployeeNo,
                MachineInsId = macDump.MachineInsId,
                MacDumpDate = macDump.MacDumpDate,
                MacDumpTime = macDump.MacDumpTime,
                EditedBy = macDump.EditedBy,
                EditedOn = macDump.EditedOn
            };

            using (_d = new DataRepository<MacDumpLog>())
            {
                if (macDump.MacDumpLogId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.MacDumpLogId;
        }
        public static int Save(List<MacDumpLog> listMacDump)
        {
            var iResult = 0;
            try
            {
                using (_d = new DataRepository<MacDumpLog>())
                {
                    foreach (var macDump in listMacDump)
                    {
                        var a = new MacDumpLog
                        {
                            MacDumpLogId = macDump.MacDumpLogId,
                            EmployeeId = macDump.EmployeeId,
                            MachineNo = macDump.MachineNo,
                            EmployeeNo = macDump.EmployeeNo,
                            MachineInsId = macDump.MachineInsId,
                            MacDumpDate = macDump.MacDumpDate,
                            MacDumpTime = macDump.MacDumpTime,
                            EditedBy = macDump.EditedBy,
                            EditedOn = macDump.EditedOn
                        };
                        _d.Add(a);
                    }
                    _d.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(@"Error occured on {0}", ex.Message));
                iResult = 0;
            }

            return iResult;
        }

        public static bool Delete(MacDumpLog macDump)
        {
            using (_d = new DataRepository<MacDumpLog>())
            {
                _d.Delete(macDump);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<MacDumpLog>())
            {
                _d.Delete(d => d.MacDumpLogId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<MacDumpLog> GetAll(string enrolleeNo, string sDt)
        {
            using (_d = new DataRepository<MacDumpLog>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.EmployeeNo == enrolleeNo && f.MacDumpDate.Trim() == sDt).ToList();
            }
        }
    }
}
