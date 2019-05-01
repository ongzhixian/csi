using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Csi.WebApp.Data
{
    public partial class CsiDbContext : DbContext
    {
        public CsiDbContext()
        {
        }

        public CsiDbContext(DbContextOptions<CsiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CsiUsers> CsiUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=CsiDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsiUsers>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConcurrencyStamp).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.NormalizedEmail).IsUnicode(false);

                entity.Property(e => e.NormalizedUserName).IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.Property(e => e.SecurityStamp).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });
        }
    }
}
