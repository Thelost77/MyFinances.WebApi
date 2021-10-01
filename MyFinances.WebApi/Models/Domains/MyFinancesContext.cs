using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyFinances.WebApi.Models.Domains
{
    public partial class MyFinancesContext : DbContext
    {
        public MyFinancesContext()
        {
        }

        public MyFinancesContext(DbContextOptions<MyFinancesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Operations)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Operations_Categories");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
