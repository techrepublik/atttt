using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class MicellaneousManager
    {
        public static DataRepository<Miscellaneou> _d;

        public static int Save(Miscellaneou miscellaneou)
        {
            var a = new Miscellaneou
            {
                MiscellaneousId = miscellaneou.MiscellaneousId,
                MiscGraceInAM = miscellaneou.MiscGraceInAM,
                MiscGraceInPM = miscellaneou.MiscGraceInPM,
                MiscGraceOT = miscellaneou.MiscGraceOT,
                MiscRegularHours = miscellaneou.MiscRegularHours,
                MiscOverRegularLabel = miscellaneou.MiscOverRegularLabel,
                MiscUnderRegularLabel = miscellaneou.MiscUnderRegularLabel,
                MiscActive = miscellaneou.MiscActive
            };

            using (_d = new DataRepository<Miscellaneou>())
            {
                if (miscellaneou.MiscellaneousId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.MiscellaneousId;
        }

        public static bool Delete(Miscellaneou miscellaneou)
        {
            using (_d = new DataRepository<Miscellaneou>())
            {
                _d.Delete(miscellaneou);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Miscellaneou>())
            {
                _d.Delete(d => d.MiscellaneousId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Miscellaneou> GetAll()
        {
            using (_d = new DataRepository<Miscellaneou>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
    }
}
