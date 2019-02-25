namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Miscellaneou
    {
        [Key]
        public int MiscellaneousId { get; set; }

        public int? MiscGraceInAM { get; set; }

        public int? MiscGraceInPM { get; set; }

        public int? MiscGraceOT { get; set; }

        public double? MiscRegularHours { get; set; }

        [StringLength(50)]
        public string MiscRegularLabel { get; set; }

        [StringLength(50)]
        public string MiscOverRegularLabel { get; set; }

        [StringLength(50)]
        public string MiscUnderRegularLabel { get; set; }

        public bool? MiscActive { get; set; }
    }
}
