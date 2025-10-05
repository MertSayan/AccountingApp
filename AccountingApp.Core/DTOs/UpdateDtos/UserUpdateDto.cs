using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs.UpdateDtos
{
    public class UserUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GroupId { get; set; }
    }
}
