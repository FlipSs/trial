using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Xm.Trial.Services;
using Xm.Trial.Models.Data;
using System.Web;

namespace Xm.Trial
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IMailSender, SendMailService>();
            container.RegisterType<DataContext>();
            container.RegisterInstance(new AppConfiguration(HttpContext.Current.Server.MapPath("appsettings.json")));
            container.RegisterSingleton<IAppConfiguration, AppConfiguration>();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}