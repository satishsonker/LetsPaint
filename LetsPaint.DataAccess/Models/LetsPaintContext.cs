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

        public virtual DbSet<MstGallery> MstGallery { get; set; }
        public virtual DbSet<MstGalleryType> MstGalleryType { get; set; }
        public virtual DbSet<MstRefLookup> MstRefLookup { get; set; }
        public virtual DbSet<MstUserDetails> MstUserDetails { get; set; }
        public virtual DbSet<MstUserType> MstUserType { get; set; }
        public virtual DbSet<MstUsers> MstUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=satishsonkar;Database=letspaint;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<MstGallery>(entity =>
            {
                entity.HasKey(e => e.GalleryId);

                entity.ToTable("Mst_Gallery");

                entity.Property(e => e.AvailableQy).HasDefaultValueSql("((1))");

                entity.Property(e => e.Badge)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("(N'New')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.HasArtistCertificate)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.HasArtistSign)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Medium).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Size).HasMaxLength(50);

                entity.Property(e => e.SuitableFor).HasMaxLength(500);

                entity.Property(e => e.Surface).HasMaxLength(10);

                entity.Property(e => e.Tags).HasMaxLength(500);

                entity.Property(e => e.Thumbnail)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.MstGalleryArtist)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_Gallery_Mst_Users1");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MstGalleryCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_Gallery_Mst_Users");

                entity.HasOne(d => d.GalleyType)
                    .WithMany(p => p.MstGallery)
                    .HasForeignKey(d => d.GalleyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_Gallery_Mst_GalleryType");
            });

            modelBuilder.Entity<MstGalleryType>(entity =>
            {
                entity.HasKey(e => e.GalleryTypeId);

                entity.ToTable("Mst_GalleryType");

                entity.Property(e => e.BaseUrl).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.GalleryType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.GridSize).HasDefaultValueSql("((4))");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<MstRefLookup>(entity =>
            {
                entity.HasKey(e => e.RefId);

                entity.ToTable("Mst_RefLookup");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RefKey)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RefValue)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.MstRefLookup)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mst_RefLookup_Mst_Users");
            });

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

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Mobile).HasMaxLength(13);

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
