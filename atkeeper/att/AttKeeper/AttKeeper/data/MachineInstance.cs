namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MachineInstance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MachineInstance()
        {
            Machines = new HashSet<Machine>();
        }

        [Key]
        public int MachineInsId { get; set; }

        [StringLength(50)]
        public string MachineInsName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MachineInsDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
