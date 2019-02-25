namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MacDumpLog
    {
        public int MacDumpLogId { get; set; }

        public int? EmployeeId { get; set; }

        public int? MachineNo { get; set; }

        public string EmployeeNo { get; set; }

        public int? MachineInsId { get; set; }

        [StringLength(50)]
        public string MacDumpDate { get; set; }

        [StringLength(50)]
        public string MacDumpTime { get; set; }

        [StringLength(50)]
        public string EditedBy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EditedOn { get; set; }

        public virtual Empoyee Empoyee { get; set; }
    }
}
