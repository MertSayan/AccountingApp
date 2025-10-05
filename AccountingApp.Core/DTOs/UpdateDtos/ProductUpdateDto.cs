using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs.UpdateDtos
{
    public class ProductUpdateDto : BaseDto
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
    }
}
