using System;
using System.Collections.Generic;

namespace LabEFCore.Models
{
    public partial class RoleActions
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int WebActionId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual WebActions WebAction { get; set; }
    }
}
