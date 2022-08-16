using System;
using AccountingChildrens.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingChildrens.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Children> Childrens { get; set; }
        public DbSet<Educator> Educators { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChildrenGroup>()
                .HasKey(t => new { t.ChildrenId, t.GroupId });

            modelBuilder.Entity<EducatorGroup>()
                .HasKey(t => new { t.EducatorId, t.GroupId });

            modelBuilder.Entity<Children>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.FirstName)
                    .HasMaxLength(25)
                    .IsRequired();
                p.Property(x => x.LastName)
                    .HasMaxLength(25)
                    .IsRequired();
                p.Property(x => x.Age)
                    .IsRequired();
            });

            modelBuilder.Entity<Educator>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.FirstName)
                    .HasMaxLength(25)
                    .IsRequired();
                p.Property(x => x.LastName)
                    .HasMaxLength(25)
                    .IsRequired();
            });

            modelBuilder.Entity<Group>(p =>
            {
                p.HasKey(x => x.Id);
                p.Property(x => x.Name)
                    .HasMaxLength(25)
                    .IsRequired();
            });

        }
    }
}

