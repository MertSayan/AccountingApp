using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Models
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid GroupId { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public Department Department { get; set; }
        public Group Group { get; set; }
    }
}
