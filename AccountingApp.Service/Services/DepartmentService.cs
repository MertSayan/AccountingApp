using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;

namespace AccountingApp.Service.Services
{
    public class DepartmentService(IGenericRepository<Department> repository, IUnitOfWorks unitOfWorks, IDepartmentRepository departmentRepository) : Service<Department>(repository, unitOfWorks), IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository = departmentRepository;
    }
}
