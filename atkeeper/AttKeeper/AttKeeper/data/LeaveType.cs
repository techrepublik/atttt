namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LeaveType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveType()
        {
            Leaves = new HashSet<Leaf>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeaveTypeId { get; set; }

        [StringLength(100)]
        public string LeaveTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Leaf> Leaves { get; set; }
    }
}
