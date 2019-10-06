using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Csi.Data
{
    public partial class MySqlDbContext : DbContext
    {
        public MySqlDbContext()
        {
        }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=CsiDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ZX:  Added converters to resolve the folllowing error message.
            //      Unable to cast object of type 'System.Boolean' to type 'System.Int16'
            //      Later learnt that this is not the correct approach to handle this issue.
            //      The issue here is really with the MySql provider.
            //      When generating the SQL for the Identity class (CsiUser),
            //      it (incorrectly) convert .NET bool type to short (Int16).
            //      A much better approach maybe override these specific fields in 
            //      CsiUser.cs class and add annotations specifying the data type:
            //      [Column(TypeName = "bit(1)")]

            // modelBuilder.Entity<CsiUser>()
            //     .Property(r => r.EmailConfirmed)
            //     .HasConversion(new BoolToZeroOneConverter<Int16>());
            // modelBuilder.Entity<CsiUser>()
            //     .Property(r => r.PhoneNumberConfirmed)
            //     .HasConversion(new BoolToZeroOneConverter<Int16>());
            // modelBuilder.Entity<CsiUser>()
            //     .Property(r => r.TwoFactorEnabled)
            //     .HasConversion(new BoolToZeroOneConverter<Int16>());
            // modelBuilder.Entity<CsiUser>()
            //     .Property(r => r.LockoutEnabled)
            //     .HasConversion(new BoolToZeroOneConverter<Int16>());

        }

        // Add DbSets here 
        //public virtual DbSet<CsiUser> CsiUsers { get; set; }
        //public virtual DbSet<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
        //public DbSet<Csi.Data.Project> Project { get; set; }

    }

}