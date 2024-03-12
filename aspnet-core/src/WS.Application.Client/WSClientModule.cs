using Abp.Modules;
using Abp.Reflection.Extensions;

namespace WS
{
    public class WSClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(WSClientModule).GetAssembly());
        }
    }
}
