using AssetPortfolio.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetPortfolio.Infrastructure.Data;

public partial class AssetPortfolioDbContext : DbContext
{
    public AssetPortfolioDbContext(DbContextOptions<AssetPortfolioDbContext> options): base(options)
    {
    }

    public virtual DbSet<ApplicationErrorLog> ApplicationErrorLogs { get; set; }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAsset> UserAssets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationErrorLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07842B2EA1");

            entity.ToTable("ApplicationErrorLogs", "Core");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assets__3214EC0703B1AA1A");

            entity.ToTable("Assets", "Core");

            entity.Property(e => e.AssetName).HasMaxLength(30);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PriceUsd)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Price(USD)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Quantity);

            entity.HasOne(d => d.AssetType).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssetTypeId)
                .HasConstraintName("FK__Assets__AssetTyp__47DBAE45");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetTyp__3214EC071202E761");

            entity.ToTable("AssetTypes", "Core");

            entity.Property(e => e.AssetTypeName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A667C306");

            entity.ToTable("Users", "Core");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105344C6923DB").IsUnique();

            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserAsset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAsse__3214EC07ADF78178");

            entity.ToTable("UserAssets", "Core");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Asset).WithMany(p => p.UserAssets)
                .HasForeignKey(d => d.AssetId)
                .HasConstraintName("FK__UserAsset__Asset__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.UserAssets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserAsset__UserI__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
