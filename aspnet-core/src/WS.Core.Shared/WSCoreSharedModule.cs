using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    public class WSCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSCoreSharedModule).GetAssembly());
        }
    }
}