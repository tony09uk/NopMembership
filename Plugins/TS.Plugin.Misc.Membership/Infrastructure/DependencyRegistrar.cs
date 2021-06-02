using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Ts.Plugin.Misc.Membership.Factories;
using Ts.Plugin.Misc.Membership.Services;

namespace Ts.Plugin.Misc.Membership.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            RegisterControllers(builder);
            RegisterFactories(builder);
            RegisterServices(builder);
        }

        public int Order => 100;

        private void RegisterControllers(ContainerBuilder builder)
        {
        }

        private void RegisterFactories(ContainerBuilder builder)
        {
            builder.RegisterType<MembershipNavigationFactory>().As<IMembershipNavigationFactory>();
            builder.RegisterType<MembershipListFactory>().As<IMembershipListFactory>();
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<MembershipService>().As<IMembershipService>();
            builder.RegisterType<WidgetSelectorService>().As<IWidgetSelectorService>();
            builder.RegisterType<OrderAndCompleteService>().As<IOrderAndCompleteService>();
        }
    }
}
