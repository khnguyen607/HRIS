using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using WS.Authorization;

namespace WS
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(WSApplicationSharedModule),
        typeof(WSCoreModule)
        )]
    public class WSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSApplicationModule).GetAssembly());
        }
    }
}