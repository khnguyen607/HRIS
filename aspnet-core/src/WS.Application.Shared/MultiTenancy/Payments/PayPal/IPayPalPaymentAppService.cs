using System.Threading.Tasks;
using Abp.Application.Services;
using WS.MultiTenancy.Payments.PayPal.Dto;

namespace WS.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
