using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using planets.Services;
using Autofac;
using Autofac.Util;
using planets.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace planets
{
    public partial class App : Application
    {
        public static IContainer Container;
        static readonly ContainerBuilder Builder = new ContainerBuilder();

        public App()
        {
            InitializeComponent();
            DependencyResolver.ResolveUsing(type =>
                Container.IsRegistered(type) ? Container.Resolve(type) : null);
            MainPage = new AppShell();
        }

        public static void RegisterType<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            Builder.RegisterType<T>().As<TInterface>();
        }

        public static void BuildContainer()
        {
            RegisterSharedTypes();
            Container = Builder.Build();
        }

        private static void RegisterSharedTypes()
        {
            RegisterServices();
            RegisterViewModels();
        }

        private static void RegisterServices()
        {
            RegisterTypes(typeof(IService).GetTypeInfo().Assembly, "Service");
        }

        private static void RegisterViewModels()
        {
            var viewModelsassembly = typeof(BaseViewModel).GetTypeInfo().Assembly;
            var viewModels = viewModelsassembly.GetLoadableTypes()
                .Where(x => x.IsAssignableTo<BaseViewModel>() && x != typeof(BaseViewModel));
            foreach (var type in viewModels)
            {
                Builder.RegisterType(type);
            }
        }

        private static void RegisterTypes(Assembly assembly, string endsWith)
        {
            Builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith(endsWith))
                .AsImplementedInterfaces();
        }
    }
}
