namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Empoyee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empoyee()
        {
            DTRs = new HashSet<DTR>();
            MacDumpLogs = new HashSet<MacDumpLog>();
        }

        [Key]
        public int EmployeeId { get; set; }

        [StringLength(50)]
        public string EmployeeNo { get; set; }

        [StringLength(50)]
        public string EmployeeIdNo { get; set; }

        [StringLength(50)]
        public string EmployeeLastName { get; set; }

        [StringLength(50)]
        public string EmployeeFirstName { get; set; }

        [StringLength(50)]
        public string EmployeeMiddleName { get; set; }

        public string EmployeeAddress { get; set; }

        [StringLength(50)]
        public string EmployeeSex { get; set; }

        [Column(TypeName = "image")]
        public byte[] EmployeePhoto { get; set; }

        public bool? EmployeeIsActive { get; set; }

        public int? SettingId { get; set; }

        public int? PositionId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DTR> DTRs { get; set; }

        public virtual Position Position { get; set; }

        public virtual Setting Setting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MacDumpLog> MacDumpLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Machine> Machines { get; set; }
    }
}
