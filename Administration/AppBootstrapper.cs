using System.Reflection;
using Administration.Utils;
using Autofac;
using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Connection;

namespace Administration
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

        protected override void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBoxService.ShowError(e.Exception.InnerException);
            e.Handled = true;
        }

        private IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces().AsSelf().PropertiesAutowired(
                    PropertyWiringFlags.PreserveSetValues);
            containerBuilder.RegisterType<ConnectionProvider>().SingleInstance().AsImplementedInterfaces();

            containerBuilder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            containerBuilder.RegisterType<WindowManager>().As<IWindowManager>();
            containerBuilder.Register(cc => _container).ExternallyOwned();
            return containerBuilder.Build();
        }
    }
}