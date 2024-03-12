using System.Threading.Tasks;
using WS.Sessions.Dto;

namespace WS.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
