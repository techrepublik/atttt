namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DTR
    {
        public int DTRId { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DTRDate { get; set; }

        [StringLength(50)]
        public string DTRDay { get; set; }

        [StringLength(50)]
        public string TimeInAM { get; set; }

        [StringLength(50)]
        public string TimeOutAM { get; set; }

        [StringLength(50)]
        public string TimeInPM { get; set; }

        [StringLength(50)]
        public string TimeOutPM { get; set; }

        [StringLength(50)]
        public string TimeInOT { get; set; }

        [StringLength(50)]
        public string TimeOutOT { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalMinute { get; set; }

        public double? TotalHour { get; set; }

        [StringLength(50)]
        public string DTRStatus { get; set; }

        public bool? IsSource { get; set; }

        [StringLength(50)]
        public string EditedBy { get; set; }

        public DateTime? EditedOn { get; set; }

        public virtual Empoyee Empoyee { get; set; }
    }
}
