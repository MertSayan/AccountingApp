using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;

namespace AccountingApp.Service.Services
{
    public class UserService(IGenericRepository<User> repository, IUnitOfWorks unitOfWorks) : GenericService<User>(repository, unitOfWorks), IUserService
    {
    }
}
