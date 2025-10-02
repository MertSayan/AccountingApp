using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;

namespace AccountingApp.Service.Services
{
    public class GroupInRoleService(IGenericRepository<GroupInRole> repository, IUnitOfWorks unitOfWorks) : Service<GroupInRole>(repository, unitOfWorks), IGroupInRoleService
    {
    }
}
