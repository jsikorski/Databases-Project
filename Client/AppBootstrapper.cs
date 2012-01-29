using System.Reflection;
using Autofac;
using Common;
using Connection;

namespace Client
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.ComponentModel.Composition.Primitives;
	using System.Linq;
	using Caliburn.Micro;

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

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired(
                    PropertyWiringFlags.PreserveSetValues);
            containerBuilder.RegisterType<SymbolsProvider>().AsImplementedInterfaces();
            containerBuilder.RegisterType<ConnectionProvider>().SingleInstance().AsImplementedInterfaces();
            //containerBuilder.RegisterType<MainViewModel>().SingleInstance().AsSelf().AsImplementedInterfaces();
            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            containerBuilder.RegisterType<WindowManager>().As<IWindowManager>();
            containerBuilder.Register(cc => _container).ExternallyOwned();
            return containerBuilder.Build();
        }
    }
}
