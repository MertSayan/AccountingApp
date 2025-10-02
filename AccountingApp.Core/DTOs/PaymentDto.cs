using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOs
{
    public class PaymentDto:BaseDto
    {
        public Guid CustomerId { get; set; }
        public double Amount { get; set; }

        public CustomerDto Customet { get; set; }
    }
}
