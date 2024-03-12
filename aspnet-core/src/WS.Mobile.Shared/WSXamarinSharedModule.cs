using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    [DependsOn(typeof(WSClientModule), typeof(AbpAutoMapperModule))]
    public class WSXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSXamarinSharedModule).GetAssembly());
        }
    }
}