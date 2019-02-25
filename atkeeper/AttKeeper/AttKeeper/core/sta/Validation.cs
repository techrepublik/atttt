using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.core.sta
{
    class Validation
    {
        public static User GetActiveUser(string username, string password)
        {
            using (var _d = new BioModel())
            {
                return _d.Users.FirstOrDefault(
                        f => f.UserName == username && f.Password == password && f.UserIsActive == true);
            }
        }
    }
}
