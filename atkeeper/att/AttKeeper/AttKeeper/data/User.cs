namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int UserId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public int? UserLevel { get; set; }

        [StringLength(150)]
        public string UserFullName { get; set; }

        public bool? UserIsActive { get; set; }
    }
}
