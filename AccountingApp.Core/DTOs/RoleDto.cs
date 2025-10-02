using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs
{
    public class RoleDto:BaseDto
    {
        public string Name { get; set; }

        public ICollection<GroupInRole> GroupInRoles { get; set; }
    }
}
