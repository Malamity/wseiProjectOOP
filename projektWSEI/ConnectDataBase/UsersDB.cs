using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace ConnectDataBase
{
    public partial class UsersDB : DbContext
    {
        public virtual DbSet<Employers> Employers { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Performance> Performance { get; set; }
        public virtual DbSet<Performance_staff> Performance_staff { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string pathToDB = Path.Combine(Environment.CurrentDirectory, "ClubDB.mdf");
                optionsBuilder.UseSqlServer($@"Server=(LocalDB)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName={pathToDB}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employers>()
                .Property(e => e.Employer_name)
                .HasMaxLength(50);

            modelBuilder.Entity<Employers>()
                .Property(e => e.Employer_last_name)
                .HasMaxLength(50);

            modelBuilder.Entity<Employers>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Employers>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Employers>()
                .HasMany(e => e.Performance_staff)
                .WithOne(a => a.Employers)
                .HasForeignKey(a => a.Employer_id);

            modelBuilder.Entity<Items>()
                .Property(e => e.Item_name)
                .HasMaxLength(50);

            modelBuilder.Entity<Items>()
                .HasMany(e => e.Performance)
                .WithMany(e => e.Items);

            modelBuilder.Entity<Performance>()
                .Property(e => e.Performance_name)
                .HasMaxLength(50);

            modelBuilder.Entity<Performance>()
                .Property(e => e.Performace_visit_cost)
                .HasPrecision(10, 4);

            modelBuilder.Entity<Performance>()
                .HasMany(e => e.Performance_staff)
                .WithOne(e => e.Performance)
                .HasForeignKey(a=>a.Performance_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Employers>()
                .HasData(new Employers()
                {
                    Employer_id=1,
                    Employer_name ="Ryszard",
                    Employer_last_name = "Brzegowy",
                    Employment_date = DateTime.Now,
                    Password="admin",
                    Username="admin"
                });


        }
    }
}
