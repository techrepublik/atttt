using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class CompanyManager
    {
        public static DataRepository<Company> _d;

        public static int Save(Company company)
        {
            var a = new Company
            {
                CompanyId = company.CompanyId,
                CompanyName = company.CompanyName,
                CompanyAddress = company.CompanyAddress,
                CompanyPrincipal = company.CompanyPrincipal,
                CompanyLogo1 = company.CompanyLogo1,
                CompanyLogo2 = company.CompanyLogo2
            };

            using (_d = new DataRepository<Company>())
            {
                if (company.CompanyId > 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.CompanyId;
        }

        public static bool Delete(Company company)
        {
            using (_d = new DataRepository<Company>())
            {
                _d.Delete(company);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<Company>())
            {
                _d.Delete(d => d.CompanyId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<Company> GetAll()
        {
            using (_d = new DataRepository<Company>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.GetAll().ToList();
            }
        }
        public static Company Get()
        {
            using (_d = new DataRepository<Company>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.FirstOrDefault(f => f.CompanyId == 1) ;
            }
        }
    }
}
