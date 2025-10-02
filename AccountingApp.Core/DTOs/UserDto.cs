using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs
{
    public class UserDto:BaseDto
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GroupId { get; set; }

        public DepartmentDto Department { get; set; }
        public GroupDto Group { get; set; }
    }
}
