using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Authorization.Permissions.Dto;

namespace WS.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
