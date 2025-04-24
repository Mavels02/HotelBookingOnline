using Microsoft.EntityFrameworkCore;

namespace API_HotelBooking.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<DatDichVu> DatDichVus { get; set; }
        public DbSet<DatPhong> DatPhongs { get; set; }
        public DbSet<Phong> Phongs { get; set; }
        public DbSet<LoaiPhong> LoaiPhongs { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ 1-1: ThanhToan - DatPhong
            modelBuilder.Entity<ThanhToan>()
                .HasOne(t => t.DatPhong)
                .WithOne(dp => dp.ThanhToan)
                .HasForeignKey<ThanhToan>(t => t.MaDP);

            // Cấu hình quan hệ 1-n: DatDichVu - DichVu, DatPhong
            modelBuilder.Entity<DatDichVu>()
                .HasOne(ddv => ddv.DichVu)
                .WithMany(dv => dv.DatDichVus)
                .HasForeignKey(ddv => ddv.MaDV);

            modelBuilder.Entity<DatDichVu>()
                .HasOne(ddv => ddv.DatPhong)
                .WithMany(dp => dp.DatDichVus)
                .HasForeignKey(ddv => ddv.MaDP);

         
        }
    }
}
