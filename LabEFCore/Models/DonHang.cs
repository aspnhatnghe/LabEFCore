using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabEFCore.Models
{
    [Table("DonHang")]
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public int MaDh { get; set; }
        public string MaKh { get; set; }
        public DateTime NgayDatHang { get; set; }
        public DateTime NgayCan { get; set; }
        public string NguoiNhanHang { get; set; }
        public string DiaChi { get; set; }
        public string MoTa { get; set; }
        public double TongTien { get; set; }

        [ForeignKey("MaKh")]
        public virtual KhachHang KhachHang { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
