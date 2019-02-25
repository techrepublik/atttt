using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class PositionManager
    {
        public static DataRepository<Position> _d;

        public static int Save(Position position)
        {
            var a = new Position
            {
                PositionId = position.PositionId,
                PositionName = position.PositionName,
                PositionIsActive = position.PositionIsActive,
                DepartmentId = position.DepartmentId
            };

            using (_d = new DataRepository<Position>())
            {
                if (position.PositionId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.PositionId;
        }

        public static bool Delete(Position position)
        {
            using (_d = new DataRepository<Position>())
            {
                _d.Delete(position);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Position>())
            {
                _d.Delete(d => d.PositionId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Position> GetAll()
        {
            using (_d = new DataRepository<Position>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().OrderBy(o => o.PositionName).ToList();
            }
        }
        public static List<Position> GetAll(int iId)
        {
            using (_d = new DataRepository<Position>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.DepartmentId == iId).ToList();
            }
        }
        public static Position Get(int? iId)
        {
            using (_d = new DataRepository<Position>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.FirstOrDefault(f => f.PositionId == iId);
            }
        }
    }
}
