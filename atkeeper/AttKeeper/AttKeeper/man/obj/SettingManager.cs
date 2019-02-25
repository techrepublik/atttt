using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    public class SettingManager
    {
        public static DataRepository<Setting> _d;

        public static int Save(Setting setting)
        {
            var a = new Setting
            {
                SettingId = setting.SettingId,
                SettingName = setting.SettingName
            };

            using (_d = new DataRepository<Setting>())
            {
                if (setting.SettingId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.SettingId;
        }

        public static bool Delete(Setting setting)
        {
            using (_d = new DataRepository<Setting>())
            {
                _d.Delete(setting);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Setting>())
            {
                _d.Delete(d => d.SettingId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Setting> GetAll()
        {
            using (_d = new DataRepository<Setting>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
    }
}
