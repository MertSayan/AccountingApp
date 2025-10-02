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
    public class GroupService(IGenericRepository<Group> repository, IUnitOfWorks unitOfWorks) : Service<Group>(repository, unitOfWorks),IGroupService
    {
    }
}
