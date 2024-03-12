using System.Threading.Tasks;
using Abp.Application.Services;
using WS.MultiTenancy.Payments.Dto;
using WS.MultiTenancy.Payments.Stripe.Dto;

namespace WS.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}