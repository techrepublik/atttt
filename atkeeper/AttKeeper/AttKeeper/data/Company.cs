namespace AttKeeper.data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPrincipal { get; set; }

        [Column(TypeName = "image")]
        public byte[] CompanyLogo1 { get; set; }

        [Column(TypeName = "image")]
        public byte[] CompanyLogo2 { get; set; }
    }
}
