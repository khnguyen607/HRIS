using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Common.Dto;
using WS.Editions.Dto;

namespace WS.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}