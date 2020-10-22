using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using SportsStore.Domain.Databases.EntityFramework.Repositories;
using SportsStore.Domain.Databases.Dapper;
using SportsStore.Domain.Databases.Dapper.Providers;
using SportsStore.Domain.Processors;
using SportsStore.WebUI.Infrastructure.Authentication;

namespace SportsStore.WebUI.Infrastructure
{
    public class SportsStoreDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public SportsStoreDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;

            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IProductRepository>().To<ProductRepository>();

            var emailSettings = DependencyDataGenerator.GenerateEmailSettings();
            _kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("emailSettings", emailSettings);

            _kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            _kernel.Bind<IConnectionStringSettings>().To<ConnectionStringSettings>();
            _kernel.Bind<IDataAccessProvider>().To<DataAccessProvider>();
        }
    }
}