using AccountingApp.Core.Repositories;
using AccountingApp.Core.Services;
using AccountingApp.Core.UnitOfWorks;
using AccountingApp.Repository;
using AccountingApp.Repository.Repositories;
using AccountingApp.Repository.UnitOfWorks;
using AccountingApp.Service.Mappings;
using AccountingApp.Service.Services;
using Autofac;
using System.Reflection;
using System.Xml.Linq;
using Module = Autofac.Module;

namespace AccountingApp.API.Modules
{
    public class RepoServiceModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
