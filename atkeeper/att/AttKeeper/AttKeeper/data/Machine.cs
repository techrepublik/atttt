namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Machine
    {
        public int MachineId { get; set; }

        public int? MachineInsId { get; set; }

        public int? EmployeeId { get; set; }

        public int? MachineNo { get; set; }

        public string EnrolleeNo { get; set; }

        public int? IYear { get; set; }

        public int? IMonth { get; set; }

        public int? IDay { get; set; }

        public int? IHour { get; set; }

        public int? IMin { get; set; }

        public int? ISec { get; set; }

        public int? InOutCode { get; set; }

        public int? VerifyCode { get; set; }

        public virtual MachineInstance MachineInstance { get; set; }
        public virtual Empoyee Employee { get; set; }
    }
}
