using System.Collections.Generic;
using System.Linq;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    public class DepartmentManager
    {
        public static DataRepository<Department> _d;

        public static int Save(Department department)
        {
            var a = new Department
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                DepartmentHead = department.DepartmentHead,
                CompanyId = department.CompanyId
            };

            using (_d = new DataRepository<Department>())
            {
                if (department.DepartmentId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.DepartmentId;
        }

        public static bool Delete(Department department)
        {
            using (_d = new DataRepository<Department>())
            {
                _d.Delete(department);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Department>())
            {
                _d.Delete(d => d.CompanyId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Department> GetAll()
        {
            using (_d = new DataRepository<Department>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
    }
}
