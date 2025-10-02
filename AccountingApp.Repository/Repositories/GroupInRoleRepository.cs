using AccountingApp.Core.Models;
using AccountingApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Repository.Repositories
{
    public class GroupInRoleRepository(AppDbContext context):GenericRepository<GroupInRole>(context),IGroupInRoleRepository
    {
    }
}
