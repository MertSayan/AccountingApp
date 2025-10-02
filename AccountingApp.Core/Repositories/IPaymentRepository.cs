using AccountingApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountingApp.Core.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}
