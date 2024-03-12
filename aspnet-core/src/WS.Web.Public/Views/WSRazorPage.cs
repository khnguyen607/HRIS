using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace WS.Web.Public.Views
{
    public abstract class WSRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected WSRazorPage()
        {
            LocalizationSourceName = WSConsts.LocalizationSourceName;
        }
    }
}
