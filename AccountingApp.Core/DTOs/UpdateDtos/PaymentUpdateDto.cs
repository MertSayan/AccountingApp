using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs.UpdateDtos
{
    public class PaymentUpdateDto:BaseDto
    {
        public Guid CustomerId { get; set; }
        public double Amount { get; set; }
    }
}
