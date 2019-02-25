using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttKeeper.data;

namespace AttKeeper.man.obj
{
    class SettingDetailManager
    {
        public static DataRepository<SettingDetail> _d;

        public static int Save(SettingDetail dettingDetail)
        {
            var a = new SettingDetail
            {
                SettingDetailId = dettingDetail.SettingDetailId,
                SettingId = dettingDetail.SettingId,
                SettingDetailDay = dettingDetail.SettingDetailDay,
                SettingDetailAMIn01 = dettingDetail.SettingDetailAMIn01,
                SettingDetailAMIn02 = dettingDetail.SettingDetailAMIn02,
                SettingDetailAMOut01 = dettingDetail.SettingDetailAMOut01,
                SettingDetailAMOut02 = dettingDetail.SettingDetailAMOut02,
                SettingDetailPMIn01 = dettingDetail.SettingDetailPMIn01,
                SettingDetailPMIn02 = dettingDetail.SettingDetailPMIn02,
                SettingDetailPMOut01 = dettingDetail.SettingDetailPMOut01,
                SettingDetailPMOut02 = dettingDetail.SettingDetailPMOut02,
                SettingDetailOTIn01 = dettingDetail.SettingDetailOTIn01,
                SettingDetailOTin02 = dettingDetail.SettingDetailOTin02,
                SettingDetailOTOut01 = dettingDetail.SettingDetailOTOut01,
                SettingDetailOTOut02 = dettingDetail.SettingDetailOTOut02,
                SettingDetailINAM = dettingDetail.SettingDetailINAM,
                SettingDetailOUTAM = dettingDetail.SettingDetailOUTAM,
                SettingDetailINPM = dettingDetail.SettingDetailINPM,
                SettingDetailOUTPM = dettingDetail.SettingDetailOUTPM,
                SettingDetailINOT = dettingDetail.SettingDetailINOT,
                SettingDetailOUTOT = dettingDetail.SettingDetailOUTOT,
                SettingDetailActive = dettingDetail.SettingDetailActive
            };

            using (_d = new DataRepository<SettingDetail>())
            {
                if (dettingDetail.SettingDetailId> 0)
                    _d.Update(a);
                else _d.Add(a);

                _d.SaveChanges();
            }

            return a.SettingDetailId;
        }

        public static bool Delete(SettingDetail settingDetail)
        {
            using (_d = new DataRepository<SettingDetail>())
            {
                _d.Delete(settingDetail);
                _d.SaveChanges();
            }

            return true;
        }

        public static bool Delete(int iId)
        {
            using (_d = new DataRepository<SettingDetail>())
            {
                _d.Delete(d => d.SettingDetailId == iId);
                _d.SaveChanges();
            }

            return true;
        }

        public static List<SettingDetail> GetAll(int? iId)
        {
            using (_d = new DataRepository<SettingDetail>())
            {
                _d.LazyLoadingEnabled = false;
                return _d.Find(f => f.SettingId == iId).OrderByDescending(o => o.SettingDetailId).ToList();
            }
        }
    }
}
