using System.Threading.Tasks;
using Abp.Application.Services;
using WS.Editions.Dto;
using WS.MultiTenancy.Dto;

namespace WS.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}