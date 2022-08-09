using System.Web.Mvc;
using Abstracts.Models.InterfaceService;
using Abstracts.Models.Service;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace Abstracts
{
    public static class Services
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            return container;
        }
    }
}