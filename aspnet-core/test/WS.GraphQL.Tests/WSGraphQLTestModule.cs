using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WS.Configure;
using WS.Startup;
using WS.Test.Base;

namespace WS.GraphQL.Tests
{
    [DependsOn(
        typeof(WSGraphQLModule),
        typeof(WSTestBaseModule))]
    public class WSGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSGraphQLTestModule).GetAssembly());
        }
    }
}