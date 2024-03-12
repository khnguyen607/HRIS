using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    [DependsOn(typeof(WSCoreSharedModule))]
    public class WSApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSApplicationSharedModule).GetAssembly());
        }
    }
}