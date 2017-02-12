using Castle.Windsor;
using Castle.Windsor.Installer;

namespace csvTransformer.Container
{
    public static class IocContainer
    {
        private static IWindsorContainer _container;

        public static void Setup()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}