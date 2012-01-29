using System.Reflection;
using Autofac;
using Client.Features.Flights;
using Common;
using Connection;
using System;
using System.Collections.Generic;
using Caliburn.Micro;

namespace Client
{
	public class AppBootstrapper : Bootstrapper<IShell>
    {
        private IContainer _container;

        protected override void Configure()
        {
            _container = CreateContainer();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.Resolve(serviceType.MakeArrayType()) as IEnumerable<object>;
        }

        private IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(type => type != typeof(FlyViewModel))
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired(
                    PropertyWiringFlags.PreserveSetValues);
            containerBuilder.RegisterType<SymbolsProvider>().AsImplementedInterfaces();
            containerBuilder.RegisterType<ConnectionProvider>().SingleInstance().AsImplementedInterfaces();
            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            containerBuilder.RegisterType<WindowManager>().As<IWindowManager>();
            containerBuilder.Register(cc => _container).ExternallyOwned();
            return containerBuilder.Build();
        }
    }
}
