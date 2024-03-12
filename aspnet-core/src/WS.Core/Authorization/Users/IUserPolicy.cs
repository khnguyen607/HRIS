using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace WS.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
