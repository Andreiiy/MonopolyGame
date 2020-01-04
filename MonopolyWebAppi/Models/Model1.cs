namespace MonopolyWebAppi4.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Filld> Fillds { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<MsgToUser> MsgToUsers { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<UserFriend> UserFriends { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filld>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<Filld>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Game>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<MsgToUser>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<MsgToUser>()
                .Property(e => e.Date)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Player>()
                .Property(e => e.Move)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.StatusInSystem)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Rank)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .IsUnicode(false);
        }
    }
}
