namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Leaf
    {
        [Key]
        public int LeaveId { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(50)]
        public string LeaveNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDateFrom { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LeaveDateTo { get; set; }

        public int? LeaveNoDays { get; set; }

        public int? LeaveTypeId { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}
