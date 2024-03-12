using Abp.AspNetCore.Mvc.ViewComponents;

namespace WS.Web.Public.Views
{
    public abstract class WSViewComponent : AbpViewComponent
    {
        protected WSViewComponent()
        {
            LocalizationSourceName = WSConsts.LocalizationSourceName;
        }
    }
}