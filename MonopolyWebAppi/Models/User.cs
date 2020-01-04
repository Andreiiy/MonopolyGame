namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string StatusInSystem { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        [Required]
        [StringLength(10)]
        public string Rank { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        public int? NumberWins { get; set; }

        public int? NumberLosses { get; set; }

        public int? Points { get; set; }
    }
}
