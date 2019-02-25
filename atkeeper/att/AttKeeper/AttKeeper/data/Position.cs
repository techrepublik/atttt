namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Position()
        {
            Empoyees = new HashSet<Empoyee>();
        }

        public int PositionId { get; set; }

        [StringLength(100)]
        public string PositionName { get; set; }

        public bool? PositionIsActive { get; set; }

        public int? DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empoyee> Empoyees { get; set; }
    }
}
