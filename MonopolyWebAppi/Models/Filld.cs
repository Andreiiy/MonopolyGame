namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Filld
    {
        public int ID { get; set; }

        public int GameID { get; set; }

        public int NumberFilld { get; set; }

        [StringLength(20)]
        public string Owner { get; set; }

        public int Price { get; set; }

        public int Rent { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
