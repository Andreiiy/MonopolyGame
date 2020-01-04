namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserFriend
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public int FreindId { get; set; }
    }
}
