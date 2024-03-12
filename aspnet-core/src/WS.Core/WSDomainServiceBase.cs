using Abp.Domain.Services;

namespace WS
{
    public abstract class WSDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected WSDomainServiceBase()
        {
            LocalizationSourceName = WSConsts.LocalizationSourceName;
        }
    }
}
