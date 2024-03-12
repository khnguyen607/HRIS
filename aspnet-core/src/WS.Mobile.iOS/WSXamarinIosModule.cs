using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    [DependsOn(typeof(WSXamarinSharedModule))]
    public class WSXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSXamarinIosModule).GetAssembly());
        }
    }
}