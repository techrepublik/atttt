using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class LeaveTypeManager
    {
        public static DataRepository<LeaveType> _d;

        public static int Save(LeaveType leaveType)
        {
            var a = new LeaveType
            {
                LeaveTypeId = leaveType.LeaveTypeId,
                LeaveTypeName = leaveType.LeaveTypeName
            };

            using (_d = new DataRepository<LeaveType>())
            {
                if (leaveType.LeaveTypeId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.LeaveTypeId;
        }

        public static bool Delete(LeaveType leaveType)
        {
            using (_d = new DataRepository<LeaveType>())
            {
                _d.Delete(leaveType);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<LeaveType>())
            {
                _d.Delete(d => d.LeaveTypeId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<LeaveType> GetAll()
        {
            using (_d = new DataRepository<LeaveType>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().OrderBy(o => o.LeaveTypeName).ToList();
            }
        }
    }
}
