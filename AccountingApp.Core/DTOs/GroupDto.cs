using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs
{
    public class GroupDto:BaseDto
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
