using System;
using System.Collections.Generic;

namespace LabEFCore.Models
{
    public partial class MasterRoles
    {
        public int Id { get; set; }
        public string MasterId { get; set; }
        public string RoleId { get; set; }

        public virtual Masters Master { get; set; }
        public virtual Roles Role { get; set; }
    }
}
