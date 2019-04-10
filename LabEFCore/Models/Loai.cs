using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabEFCore.Models
{
    [Table("Loai")]
    public partial class Loai
    {
        public Loai()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        [Display(Name ="Mã loại")]
        public int MaLoai { get; set; }
        [Display(Name = "Tên loại VN")]
        public string TenLoaiVn { get; set; }
        [Display(Name = "Tên loại EN")]
        public string TenLoaiEn { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
