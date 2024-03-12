using Abp.Application.Services;
using WS.Dto;
using WS.Logging.Dto;

namespace WS.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
