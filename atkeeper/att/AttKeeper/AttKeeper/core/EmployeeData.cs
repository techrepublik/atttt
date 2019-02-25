using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttKeeper.core
{
    class EmployeeData
    {
        public int EnrolleeId { get; set; }
        public int DepartmentId { get; set; }
        public string EnrolleeNo { get; set; }
        public string EnrolleeIdNo { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string GetFullName => FullName();

        private string FullName()
        {
            return string.Format(@"{0} {1}. {2}", FirstName, MiddleName.Substring(0,1), LastName );
        }

    }
}
