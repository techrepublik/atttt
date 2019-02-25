namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SettingDetail
    {
        public int SettingDetailId { get; set; }

        public int? SettingId { get; set; }

        [StringLength(50)]
        public string SettingDetailDay { get; set; }

        public DateTime? SettingDetailAMIn01 { get; set; }

        public DateTime? SettingDetailAMIn02 { get; set; }

        public DateTime? SettingDetailAMOut01 { get; set; }

        public DateTime? SettingDetailAMOut02 { get; set; }

        public DateTime? SettingDetailPMIn01 { get; set; }

        public DateTime? SettingDetailPMIn02 { get; set; }

        public DateTime? SettingDetailPMOut01 { get; set; }

        public DateTime? SettingDetailPMOut02 { get; set; }

        public DateTime? SettingDetailOTIn01 { get; set; }

        public DateTime? SettingDetailOTin02 { get; set; }

        public DateTime? SettingDetailOTOut01 { get; set; }

        public DateTime? SettingDetailOTOut02 { get; set; }

        public DateTime? SettingDetailINAM { get; set; }

        public DateTime? SettingDetailOUTAM { get; set; }

        public DateTime? SettingDetailINPM { get; set; }

        public DateTime? SettingDetailOUTPM { get; set; }

        public DateTime? SettingDetailINOT { get; set; }

        public DateTime? SettingDetailOUTOT { get; set; }

        public bool? SettingDetailActive { get; set; }

        public virtual Setting Setting { get; set; }
    }
}
