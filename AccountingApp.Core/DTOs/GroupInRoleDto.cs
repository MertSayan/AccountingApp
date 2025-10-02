using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs
{
    public class GroupInRoleDto:BaseDto
    {
        public Guid GroupId { get; set; }
        public Guid RoleId { get; set; }

        public Group Group { get; set; }
        public Role Role { get; set; }
    }
}
