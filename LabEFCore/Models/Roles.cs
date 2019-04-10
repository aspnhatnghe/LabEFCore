using System;
using System.Collections.Generic;

namespace LabEFCore.Models
{
    public partial class Roles
    {
        public Roles()
        {
            MasterRoles = new HashSet<MasterRoles>();
            RoleActions = new HashSet<RoleActions>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MasterRoles> MasterRoles { get; set; }
        public virtual ICollection<RoleActions> RoleActions { get; set; }
    }
}
