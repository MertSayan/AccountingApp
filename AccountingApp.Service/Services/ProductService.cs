using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Service.Services
{
    public class ProductService(IGenericRepository<Product> repository, IUnitOfWorks unitOfWorks) : GenericService<Product>(repository, unitOfWorks), IProductService
    {
    }
}
