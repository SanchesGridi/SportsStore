using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using SportsStore.WebUI.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(SportsStore.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(SportsStore.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace SportsStore.WebUI.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper _bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            _bootstrapper.Initialize(() =>
            {
                var kernel = new StandardKernel();
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                DependencyResolver.SetResolver(new SportsStoreDependencyResolver(kernel));
                return kernel;
            });
        }
        public static void Stop()
        {
            _bootstrapper.ShutDown();
        }
    }
}
