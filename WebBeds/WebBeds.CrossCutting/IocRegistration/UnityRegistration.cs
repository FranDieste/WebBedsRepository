using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebBeds.Domain.Modules.WebBedsAggregateRoot.Repositories;
using WebBeds.WebApiRepository.Base;
using WebBeds.WebApiRepository.Interfaces;
using WebBeds.WebApiRepository.Repositories;

namespace WebBeds.CrossCutting.IocRegistration
{
    public class UnityRegistration
    {
        private enum LifeTimeManagerEnum
        {
            Transient,              //An instance is created each time it is injected or resolved. 
            PerResolve,             //An instance is created by controller resolution, basically one per http request
            ContainerControlled,    //A single global instance is created throughout the life of the application
            PerThread
        }
        private static UnityRegistration _unityRegistrationInstance;
        private UnityContainer _unityContainer;

        private UnityRegistration()
        {

        }

        public static UnityRegistration UnityRegistrationInstance
        {
            get
            {
                if (_unityRegistrationInstance == null)
                    _unityRegistrationInstance = new UnityRegistration();

                return _unityRegistrationInstance;

            }
        }

        //_______________________________________________________________________________________________________________
        //Function that registers the repository interfaces with their classes
        //Thanks to this filter, you can obtain instances of the repository classes from their interfaces
        //We use Unity as a dependency injection tool
        //_______________________________________________________________________________________________________________
        public void Register()
        {

            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<IRequest, Request>();
            _unityContainer.RegisterType<IWebBedsWebApiRepository, WebBedsWebApiRepository>();

            //We register other classes whose name contains Handler (Normally all of the Application layer will be)
            var allAssembliesAndInterfaces = from a in AllClasses.FromLoadedAssemblies().ToList().Where(a => a.FullName.Contains("Handler") && a.FullName.Contains("WebBeds"))
                                             from b in a.GetInterfaces().ToList().Where(c => c.FullName.Contains("IQueryHandler"))
                                             select new { cl = a, il = b };

            var types = new Dictionary<Type, Type>();
            foreach (var item in allAssembliesAndInterfaces)
            {
                var classType = Type.GetType(item.cl.AssemblyQualifiedName);
                var interfaceType = Type.GetType(item.il.AssemblyQualifiedName);
                types.Add(classType, interfaceType);

            }

            foreach (var item in types)
            {
                _unityContainer.RegisterType(item.Value, item.Key, SetLifeTimeManager(LifeTimeManagerEnum.PerResolve));
            }

        }

        public UnityContainer GetUnityContainer()
        {
            if (_unityContainer == null)
                Register();

            return _unityContainer;

        }

        private static LifetimeManager SetLifeTimeManager(LifeTimeManagerEnum liveTimeManager)
        {
            switch (liveTimeManager)
            {
                case LifeTimeManagerEnum.ContainerControlled:
                    return new ContainerControlledLifetimeManager();
                case LifeTimeManagerEnum.PerResolve:
                    return new PerResolveLifetimeManager();

                case LifeTimeManagerEnum.PerThread:
                    return new PerThreadLifetimeManager();

                case LifeTimeManagerEnum.Transient:
                    return new TransientLifetimeManager();

                default:
                    return new TransientLifetimeManager();
            }
        }
    }
}
