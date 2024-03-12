using System.Threading.Tasks;
using Abp.Webhooks;

namespace WS.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
