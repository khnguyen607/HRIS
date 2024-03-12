using Abp.AspNetCore.Mvc.Views;

namespace WS.Web.Views
{
    public abstract class WSRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected WSRazorPage()
        {
            LocalizationSourceName = WSConsts.LocalizationSourceName;
        }
    }
}
