namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MsgToUser
    {
        public int ID { get; set; }

        public int UserIdFrom { get; set; }

        public int UserIdTo { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Message { get; set; }

        [Required]
        [StringLength(50)]
        public string Date { get; set; }
    }
}
