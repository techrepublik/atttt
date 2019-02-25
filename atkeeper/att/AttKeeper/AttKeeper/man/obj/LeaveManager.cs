using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class LeaveManager
    {
        public static DataRepository<Leaf> _d;

        public static int Save(Leaf leave)
        {
            var a = new Leaf
            {
                LeaveId = leave.LeaveId,
                EmployeeId = leave.EmployeeId,
                LeaveNo = leave.LeaveNo,
                LeaveDateFrom = leave.LeaveDateFrom,
                LeaveDateTo = leave.LeaveDateTo,
                LeaveNoDays = leave.LeaveNoDays,
                LeaveTypeId = leave.LeaveTypeId
            };

            using (_d = new DataRepository<Leaf>())
            {
                if (leave.LeaveId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.LeaveId;
        }

        public static bool Delete(Leaf leave)
        {
            using (_d = new DataRepository<Leaf>())
            {
                _d.Delete(leave);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Leaf>())
            {
                _d.Delete(d => d.LeaveId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Leaf> GetAll(int iId)
        {
            using (_d = new DataRepository<Leaf>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.EmployeeId == iId).OrderByDescending(o => o.LeaveDateFrom).ToList();
            }
        }
    }
}
