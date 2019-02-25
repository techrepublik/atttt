using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.core.sta
{
    class Queries
    {
        public static List<EmployeeData> GetEmployeeData()
        {
            using (var _d = new BioModel())
            {
                var q = from q1 in _d.Empoyees
                    select new EmployeeData
                    {
                        EnrolleeId = q1.EmployeeId,
                        EnrolleeNo = q1.EmployeeNo,
                        EnrolleeIdNo = q1.EmployeeIdNo,
                        LastName = q1.EmployeeLastName,
                        FirstName = q1.EmployeeFirstName,
                        MiddleName = q1.EmployeeMiddleName
                    };

                return q.OrderBy(o => o.LastName).ToList();
            }
        }
        public static List<EmployeeData> GetEmployeeData(int iId)
        {
            using (var _d = new BioModel())
            {
                var q = from q1 in _d.Empoyees
                    join q2 in _d.Positions on q1.PositionId equals q2.PositionId
                    join q3 in _d.Departments on q2.DepartmentId equals q3.DepartmentId
                        where q3.DepartmentId == iId    
                        select new EmployeeData
                        {
                            EnrolleeId = q1.EmployeeId,
                            EnrolleeNo = q1.EmployeeNo,
                            EnrolleeIdNo = q1.EmployeeIdNo,
                            LastName = q1.EmployeeLastName,
                            FirstName = q1.EmployeeFirstName,
                            MiddleName = q1.EmployeeMiddleName
                        };

                return q.OrderBy(o => o.LastName).ToList();
            }
        }
    }
}
