using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LabEFCore.Models;

namespace LabEFCore.Models
{
    public partial class NhatNgheShopDbContext : DbContext
    {
        public NhatNgheShopDbContext()
        {
        }

        public NhatNgheShopDbContext(DbContextOptions<NhatNgheShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }
        public virtual DbSet<MasterRoles> MasterRoles { get; set; }
        public virtual DbSet<Masters> Masters { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<RoleActions> RoleActions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<WebActions> WebActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.; Database=EShopNN; Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.MaCtDh)
                    .HasName("PK_OrderDetails");

                entity.Property(e => e.SoLuong).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.DonHang)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaDh)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaHh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDh)
                    .HasName("PK_Orders");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.MaKh)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.MoTa).HasMaxLength(1000);

                entity.Property(e => e.NgayCan).HasColumnType("datetime");

                entity.Property(e => e.NgayDatHang)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NguoiNhanHang)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.KhachHang)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.HasKey(e => e.MaHh)
                    .HasName("PK_Products");

                entity.Property(e => e.ConHieuLuc)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GiamGia).HasDefaultValueSql("(rand())");

                entity.Property(e => e.Hinh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Product.gif')");

                entity.Property(e => e.MaNcc)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'NK')");

                entity.Property(e => e.MoTa).HasMaxLength(2000);

                entity.Property(e => e.MoTaDonVi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NgaySanXuat)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoLuong).HasDefaultValueSql("((100))");

                entity.Property(e => e.TenHh)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_HangHoa_Loai1");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaNcc)
                    .HasConstraintName("FK_Products_Suppliers");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK_Customers");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Hinh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Photo.gif')");

                entity.Property(e => e.HoTen)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Loai>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK_Categories");

                entity.Property(e => e.TenLoaiEn)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TenLoaiVn)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MasterRoles>(entity =>
            {
                entity.Property(e => e.MasterId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.MasterRoles)
                    .HasForeignKey(d => d.MasterId)
                    .HasConstraintName("FK_UserInRoles_Users");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MasterRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserInRoles_Roles");
            });

            modelBuilder.Entity<Masters>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Nhất Nghệ')");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("0854774690");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK_Suppliers");

                entity.Property(e => e.MaNcc)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.DienThoai)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Logo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Logo.gif')");

                entity.Property(e => e.TenNcc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RoleActions>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleActions)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Permissions_Roles");

                entity.HasOne(d => d.WebAction)
                    .WithMany(p => p.RoleActions)
                    .HasForeignKey(d => d.WebActionId)
                    .HasConstraintName("FK_Permissions_Actions");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WebActions>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }

        public DbSet<LabEFCore.Models.HangHoaView> HangHoaView { get; set; }
    }
}
