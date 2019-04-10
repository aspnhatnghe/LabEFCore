using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabEFCore.Models
{
    [Table("ChiTietDonHang")]
    public partial class ChiTietDonHang
    {
        public int MaCtDh { get; set; }
        public int MaDh { get; set; }
        public int MaHh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double GiamGia { get; set; }

        [ForeignKey("MaDh")]
        public virtual DonHang DonHang { get; set; }
        [ForeignKey("MaHh")]
        public virtual HangHoa HangHoa { get; set; }
    }
}
