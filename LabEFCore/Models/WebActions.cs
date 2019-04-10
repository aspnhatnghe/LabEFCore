using System;
using System.Collections.Generic;

namespace LabEFCore.Models
{
    public partial class WebActions
    {
        public WebActions()
        {
            RoleActions = new HashSet<RoleActions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoleActions> RoleActions { get; set; }
    }
}
