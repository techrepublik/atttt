using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class MachineInstanceManager
    {
        public static DataRepository<MachineInstance> _d;

        public static int Save(MachineInstance machiniInstance)
        {
            var a = new MachineInstance
            {
                MachineInsId = machiniInstance.MachineInsId,
                MachineInsName = machiniInstance.MachineInsName,
                MachineInsDate = machiniInstance.MachineInsDate
            };

            using (_d = new DataRepository<MachineInstance>())
            {
                if (machiniInstance.MachineInsId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.MachineInsId;
        }

        public static bool Delete(MachineInstance machineInstance)
        {
            using (_d = new DataRepository<MachineInstance>())
            {
                _d.Delete(machineInstance);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<MachineInstance>())
            {
                _d.Delete(d => d.MachineInsId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<MachineInstance> GetAll()
        {
            using (_d = new DataRepository<MachineInstance>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
    }
}
