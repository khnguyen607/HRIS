using System.Threading.Tasks;
using WS.Authorization.Users;

namespace WS.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
