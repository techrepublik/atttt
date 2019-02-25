using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class MachineManager
    {
        public static DataRepository<Machine> _d;

        public static int Save(Machine machine)
        {
            var a = new Machine
            {
                MachineId = machine.MachineId,
                MachineInsId = machine.MachineInsId,
                EmployeeId = machine.EmployeeId,
                MachineNo = machine.MachineNo,
                EnrolleeNo = machine.EnrolleeNo,
                IYear = machine.IYear,
                IMonth = machine.IMonth,
                IDay = machine.IDay,
                IHour = machine.IHour,
                IMin = machine.IMin,
                ISec = machine.ISec,
                InOutCode = machine.InOutCode,
                VerifyCode = machine.VerifyCode
            };

            using (_d = new DataRepository<Machine>())
            {
                if (machine.MachineId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.MachineId;
        }

        public static int Save(List<Machine> listMachine)
        {
            var iResult = 0;
            try
            {
                using (_d = new DataRepository<Machine>())
                {
                    foreach (var a in listMachine.Select(machine => new Machine
                    {
                        MachineId = machine.MachineId,
                        MachineInsId = machine.MachineInsId,
                        EmployeeId = machine.EmployeeId,
                        MachineNo = machine.MachineNo,
                        EnrolleeNo = machine.EnrolleeNo,
                        IYear = machine.IYear,
                        IMonth = machine.IMonth,
                        IDay = machine.IDay,
                        IHour = machine.IHour,
                        IMin = machine.IMin,
                        ISec = machine.ISec,
                        InOutCode = machine.InOutCode,
                        VerifyCode = machine.VerifyCode
                    }))
                    {
                        _d.Add(a);
                    }
                    _d.SaveChanges();
                }
                iResult = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(@"Error occured on {0}", ex.Message));
                iResult = 0;
            }
            return iResult;
        }

        public static bool Delete(Machine machine)
        {
            using (_d = new DataRepository<Machine>())
            {
                _d.Delete(machine);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Machine>())
            {
                _d.Delete(d => d.MachineId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Machine> GetAll()
        {
            using (_d = new DataRepository<Machine>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
        public static List<Machine> GetAll(string iNo)
        {
            using (_d = new DataRepository<Machine>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.EnrolleeNo == iNo).ToList();
            }
        }
    }
}
