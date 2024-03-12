using Microsoft.Extensions.Configuration;

namespace WS.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
