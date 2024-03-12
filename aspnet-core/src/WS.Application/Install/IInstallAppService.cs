using System.Threading.Tasks;
using Abp.Application.Services;
using WS.Install.Dto;

namespace WS.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}