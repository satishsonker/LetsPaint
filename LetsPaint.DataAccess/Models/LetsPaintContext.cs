using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LetsPaint.DataAccess.Models
{
    public partial class LetsPaintContext : DbContext
    {
        public LetsPaintContext()
        {
        }

        public LetsPaintContext(DbContextOptions<LetsPaintContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MstUserDetails> MstUserDetails { get; set; }
        public virtual DbSet<MstUserType> MstUserType { get; set; }
        public virtual DbSet<MstUsers> MstUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=SATISHSONKAR;Database=LetsPaint;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<MstUserDetails>(entity =>
            {
                entity.HasKey(e => e.UserDetailsId);

                entity.ToTable("Mst_UserDetails");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookProfile).HasMaxLength(250);

                entity.Property(e => e.InstagramProfile).HasMaxLength(250);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Website).HasMaxLength(250);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MstUserDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_UserDetails_Mst_Users");
            });

            modelBuilder.Entity<MstUserType>(entity =>
            {
                entity.HasKey(e => e.UserTypeId);

                entity.ToTable("Mst_UserType");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MstUsers>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Mst_Users");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(13);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PasswordResetCode).HasMaxLength(500);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.MstUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_Users_Mst_UserType");
            });
        }
    }
}
