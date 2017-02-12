using csvTransformer.Models.Business;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace csvTransformer.Container
{
    public static class IocContainer
    {
        public static IWindsorContainer Container { get; private set; }

        public static void Setup()
        {
            Container = new WindsorContainer().Install(FromAssembly.This());
            Container.Register(Component.For<IConverter>().ImplementedBy<Converter>());
        }
    }
}