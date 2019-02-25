namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Holiday
    {
        public int HolidayId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? HolidayDate { get; set; }

        public bool? HolidayActive { get; set; }

        public string HolidayNote { get; set; }

        [StringLength(50)]
        public string HolidayType { get; set; }
    }
}
