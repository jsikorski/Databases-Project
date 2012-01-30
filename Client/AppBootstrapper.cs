using System.Reflection;
using Autofac;
using Client.Features;
using Client.Features.Flights;
using Client.Features.Reservations;
using Common;
using Common.Commands;
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

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(
                type => type != typeof (FlyViewModel) && type != typeof (ReservationViewModel))
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired(
                    PropertyWiringFlags.PreserveSetValues);
            containerBuilder.RegisterType<RemoveReservation>();
            containerBuilder.RegisterType<SymbolsProvider>().AsImplementedInterfaces();
            containerBuilder.RegisterType<ConnectionProvider>().SingleInstance().AsImplementedInterfaces();
            containerBuilder.RegisterType<MainViewModel>().SingleInstance().AsSelf().AsImplementedInterfaces();
            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            containerBuilder.RegisterType<WindowManager>().As<IWindowManager>();
            containerBuilder.Register(cc => _container).ExternallyOwned();
            return containerBuilder.Build();
        }
    }
}
