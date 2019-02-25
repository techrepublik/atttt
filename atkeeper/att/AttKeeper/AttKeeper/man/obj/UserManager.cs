using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class UserManager
    {
        public static DataRepository<User> _d;

        public static int Save(User user)
        {
            var a = new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password,
                UserLevel = user.UserLevel,
                UserFullName = user.UserFullName,
                UserIsActive = user.UserIsActive
            };

            using (_d = new DataRepository<User>())
            {
                if (user.UserId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.UserId;
        }

        public static bool Delete(User user)
        {
            using (_d = new DataRepository<User>())
            {
                _d.Delete(user);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<User>())
            {
                _d.Delete(d => d.UserId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<User> GetAll()
        {
            using (_d = new DataRepository<User>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
    }
}
