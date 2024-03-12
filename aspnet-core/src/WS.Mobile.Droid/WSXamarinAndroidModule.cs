using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    [DependsOn(typeof(WSXamarinSharedModule))]
    public class WSXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSXamarinAndroidModule).GetAssembly());
        }
    }
}