using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    public class EmployeeManager
    {
        public static DataRepository<Empoyee> _d;

        public static int Save(Empoyee employee)
        {
            var a = new Empoyee
            {
                EmployeeId = employee.EmployeeId,
                EmployeeNo = employee.EmployeeNo,
                EmployeeIdNo = employee.EmployeeIdNo,
                EmployeeLastName = employee.EmployeeLastName,
                EmployeeFirstName = employee.EmployeeFirstName,
                EmployeeMiddleName = employee.EmployeeMiddleName,
                EmployeeAddress = employee.EmployeeAddress,
                EmployeeSex = employee.EmployeeSex,
                EmployeePhoto = employee.EmployeePhoto,
                EmployeeIsActive = employee.EmployeeIsActive,
                SettingId = employee.SettingId,
                PositionId = employee.PositionId
            };

            using (_d = new DataRepository<Empoyee>())
            {
                if (employee.EmployeeId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.EmployeeId;
        }

        public static bool Delete(Empoyee empoyee)
        {
            using (_d = new DataRepository<Empoyee>())
            {
                _d.Delete(empoyee);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Empoyee>())
            {
                _d.Delete(d => d.EmployeeId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static Empoyee Get(int iId)
        {
            using (_d = new DataRepository<Empoyee>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.FirstOrDefault(f => f.EmployeeId == iId);
            }
        }

        public static List<Empoyee> GetAll()
        {
            using (_d = new DataRepository<Empoyee>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().OrderBy(o => o.EmployeeLastName).ToList();
            }
        }

        public static List<Empoyee> GetAll(bool bActive)
        {
            using (_d = new DataRepository<Empoyee>())
            {
                _d.LazyLoadingEnabled = false;
                return
                    _d.Find(f => f.EmployeeIsActive == bActive)
                        .OrderBy(o => o.EmployeeLastName).ThenBy(o => o.EmployeeFirstName)
                        .ToList();
            }
        }
    }
}