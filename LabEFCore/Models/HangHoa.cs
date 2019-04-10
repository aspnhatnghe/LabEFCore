using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabEFCore.Models
{
    [Table("HangHoa")]
    public partial class HangHoa
    {
        public HangHoa()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Display(Name = "Mã hàng hóa")]
        public int MaHh { get; set; }
        [Display(Name = "Tên hàng hóa")]
        public string TenHh { get; set; }
        [Display(Name = "Mô tả đơn vị")]
        public string MoTaDonVi { get; set; }
        [Display(Name = "Đơn giá")]
        public double DonGia { get; set; }
        [Display(Name = "Hình")]
        public string Hinh { get; set; }
        [Display(Name = "Ngày sản xuất")]
        public DateTime NgaySanXuat { get; set; }
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }
        [Display(Name = "Loại")]
        public int MaLoai { get; set; }
        [Display(Name = "Nhà cung cấp")]
        public string MaNcc { get; set; }
        [Display(Name = "Số lượng")]
        public int SoLuong { get; set; }
        [Display(Name = "Giảm giá")]
        public double GiamGia { get; set; }
        [Display(Name = "Số lần xem")]
        public int SoLanXem { get; set; }
        [Display(Name = "Còn hàng")]
        public bool? ConHieuLuc { get; set; }
        [Display(Name = "Hàng hóa đặc biệt")]
        public bool DacBiet { get; set; }
        [Display(Name = "Hàng hóa mới nhất")]
        public bool MoiNhat { get; set; }

        [ForeignKey("MaLoai")]
        public virtual Loai Loai { get; set; }
        [ForeignKey("MaNcc")]
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
