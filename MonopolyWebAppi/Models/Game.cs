namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Game
    {
        public int ID { get; set; }

        public int? PlayerRed { get; set; }

        public int? PlayerGreen { get; set; }

        public int? PlayerYellow { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
