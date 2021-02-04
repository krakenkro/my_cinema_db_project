namespace WebApplication4.Models
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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Movy> Movies { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Account1)
                .HasForeignKey(e => e.account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Category1)
                .HasForeignKey(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.Category1)
                .HasForeignKey(e => e.category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinema>()
                .HasMany(e => e.Halls)
                .WithRequired(e => e.Cinema1)
                .HasForeignKey(e => e.cinema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Seats)
                .WithRequired(e => e.Hall1)
                .HasForeignKey(e => e.hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Hall1)
                .HasForeignKey(e => e.hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Movy>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Movy>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Movy)
                .HasForeignKey(e => e.movie)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Seat1)
                .HasForeignKey(e => e.seat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Session>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Session1)
                .HasForeignKey(e => e.session)
                .WillCascadeOnDelete(false);
        }
    }
}
