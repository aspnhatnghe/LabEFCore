using System;
using System.Collections.Generic;

namespace LabEFCore.Models
{
    public partial class Masters
    {
        public Masters()
        {
            MasterRoles = new HashSet<MasterRoles>();
        }

        public string Id { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public virtual ICollection<MasterRoles> MasterRoles { get; set; }
    }
}
