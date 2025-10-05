using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs.UpdateDtos
{
    public class GroupInRoleUpdateDto:BaseDto
    {
        public Guid GroupId { get; set; }
        public Guid RoleId { get; set; }
    }
}
