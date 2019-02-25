using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class HolidayManager
    {
        public static DataRepository<Holiday> _d;

        public static int Save(Holiday holiday)
        {
            var a = new Holiday
            {
                HolidayId = holiday.HolidayId,
                HolidayDate = holiday.HolidayDate,
                HolidayActive = holiday.HolidayActive,
                HolidayNote = holiday.HolidayNote,
                HolidayType = holiday.HolidayType
            };

            using (_d = new DataRepository<Holiday>())
            {
                if (holiday.HolidayId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.HolidayId;
        }

        public static bool Delete(Holiday holiday)
        {
            using (_d = new DataRepository<Holiday>())
            {
                _d.Delete(holiday);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Holiday>())
            {
                _d.Delete(d => d.HolidayId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Holiday> GetAll()
        {
            using (_d = new DataRepository<Holiday>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().OrderByDescending(o => o.HolidayDate).ToList();
            }
        }
    }
}
