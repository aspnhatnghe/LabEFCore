using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabEFCore.Models
{
    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoas = new HashSet<HangHoa>();
        }

        public string MaNcc { get; set; }
        public string TenNcc { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string DienThoai { get; set; }

        public virtual ICollection<HangHoa> HangHoas { get; set; }
    }
}
