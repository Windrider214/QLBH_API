using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QLBH_DataAccess.Models;

namespace QLBH_DataAccess
{
    public partial class QLBH_ONLINEContext : DbContext
    {
        public QLBH_ONLINEContext()
        {
        }

        public QLBH_ONLINEContext(DbContextOptions<QLBH_ONLINEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Hoadonct> Hoadoncts { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Loaisp> Loaisps { get; set; } = null!;
        public virtual DbSet<Loaitin> Loaitins { get; set; } = null!;
        public virtual DbSet<Mediatype> Mediatypes { get; set; } = null!;
        public virtual DbSet<Medium> Media { get; set; } = null!;
        public virtual DbSet<Phanhoi> Phanhois { get; set; } = null!;
        public virtual DbSet<Sanpham> Sanphams { get; set; } = null!;
        public virtual DbSet<Thuonghieu> Thuonghieus { get; set; } = null!;
        public virtual DbSet<Tintuc> Tintucs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=DESKTOP-SQ33JI7;initial catalog=QLBH_ONLINE;user id=sa;password=123456;MultipleActiveResultSets=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.RefreshTokenExpiryTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.MaHd);

                entity.ToTable("HOADON");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(60)
                    .HasColumnName("MaHD");

                entity.Property(e => e.DiaChiKh).HasColumnName("DiaChiKH");

                entity.Property(e => e.EmailKh)
                    .HasMaxLength(200)
                    .HasColumnName("EmailKH");

                entity.Property(e => e.HoTenKh).HasColumnName("HoTenKH");

                entity.Property(e => e.LoaiThanhToan).HasMaxLength(50);

                entity.Property(e => e.MaKh)
                    .HasMaxLength(40)
                    .HasColumnName("MaKH");

                entity.Property(e => e.MaVanDon).HasMaxLength(60);

                entity.Property(e => e.NgayDat).HasColumnType("datetime");

                entity.Property(e => e.NgayGiao).HasColumnType("datetime");

                entity.Property(e => e.SdtKh)
                    .HasMaxLength(20)
                    .HasColumnName("SdtKH");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_HOADON_KHACHHANG");
            });

            modelBuilder.Entity<Hoadonct>(entity =>
            {
                entity.HasKey(e => e.Stt);

                entity.ToTable("HOADONCT");

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.MaHd)
                    .HasMaxLength(60)
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(40)
                    .HasColumnName("MaSP");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.Hoadoncts)
                    .HasForeignKey(d => d.MaHd)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_HOADONCT_HOADON");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Hoadoncts)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_HOADONCT_SANPHAM");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh);

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(40)
                    .HasColumnName("MaKH");

                entity.Property(e => e.EmailKh)
                    .HasMaxLength(200)
                    .HasColumnName("EmailKH");

                entity.Property(e => e.LoginId)
                    .HasMaxLength(450)
                    .HasColumnName("LoginID");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(20)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.LoginId)
                    .HasConstraintName("FK_KHACHHANG_AspNetUsers");
            });

            modelBuilder.Entity<Loaisp>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("LOAISP");

                entity.Property(e => e.MaLoai).HasMaxLength(40);

                entity.Property(e => e.TenLoaiSp)
                    .HasMaxLength(50)
                    .HasColumnName("TenLoaiSP");
            });

            modelBuilder.Entity<Loaitin>(entity =>
            {
                entity.HasKey(e => e.MaDm);

                entity.ToTable("LOAITIN");

                entity.Property(e => e.MaDm)
                    .HasMaxLength(40)
                    .HasColumnName("MaDM");

                entity.Property(e => e.TenDm)
                    .HasMaxLength(200)
                    .HasColumnName("TenDM");
            });

            modelBuilder.Entity<Mediatype>(entity =>
            {
                entity.ToTable("MEDIATYPE");

                entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");

                entity.Property(e => e.MediaTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.MediaId);

                entity.ToTable("MEDIA");

                entity.Property(e => e.MediaId)
                    .HasMaxLength(40)
                    .HasColumnName("MediaID");

                entity.Property(e => e.MediaTypeId).HasColumnName("MediaTypeID");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .HasConstraintName("FK_MEDIA_MEDIATYPE");
            });

            modelBuilder.Entity<Phanhoi>(entity =>
            {
                entity.HasKey(e => e.MaPh);

                entity.ToTable("PHANHOI");

                entity.Property(e => e.MaPh)
                    .HasMaxLength(50)
                    .HasColumnName("MaPH");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(40)
                    .HasColumnName("MaKH");

                entity.Property(e => e.NgayGui).HasColumnType("datetime");

                entity.Property(e => e.NgayTraLoi).HasColumnType("datetime");

                entity.Property(e => e.PhanHoi1).HasColumnName("PhanHoi");

                entity.Property(e => e.TieuDe).HasMaxLength(200);

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Phanhois)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_PHANHOI_KHACHHANG");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(40)
                    .HasColumnName("MaSP");

                entity.Property(e => e.ChiTietSp).HasColumnName("ChiTietSP");

                entity.Property(e => e.DonViTinh).HasMaxLength(50);

                entity.Property(e => e.MaLoai).HasMaxLength(40);

                entity.Property(e => e.MaTh)
                    .HasMaxLength(40)
                    .HasColumnName("MaTH");

                entity.Property(e => e.NgayThem).HasColumnType("datetime");

                entity.Property(e => e.TenSp).HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_SANPHAM_LOAISP");

                entity.HasOne(d => d.MaThNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.MaTh)
                    .HasConstraintName("FK_SANPHAM_THUONGHIEU");
            });

            modelBuilder.Entity<Thuonghieu>(entity =>
            {
                entity.HasKey(e => e.MaTh);

                entity.ToTable("THUONGHIEU");

                entity.Property(e => e.MaTh)
                    .HasMaxLength(40)
                    .HasColumnName("MaTH");

                entity.Property(e => e.AnhTh).HasColumnName("AnhTH");

                entity.Property(e => e.TenTh)
                    .HasMaxLength(100)
                    .HasColumnName("TenTH");

                entity.Property(e => e.XuatXu).HasMaxLength(50);
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.HasKey(e => e.MaTinTuc);

                entity.ToTable("TINTUC");

                entity.Property(e => e.MaTinTuc).HasMaxLength(50);

                entity.Property(e => e.AnhTt).HasColumnName("AnhTT");

                entity.Property(e => e.MaDm)
                    .HasMaxLength(40)
                    .HasColumnName("MaDM");

                entity.Property(e => e.NgayDang).HasColumnType("datetime");

                entity.Property(e => e.TieuDe).HasMaxLength(200);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.MaDmNavigation)
                    .WithMany(p => p.Tintucs)
                    .HasForeignKey(d => d.MaDm)
                    .HasConstraintName("FK_TINTUC_LOAITIN");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tintucs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_TINTUC_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
