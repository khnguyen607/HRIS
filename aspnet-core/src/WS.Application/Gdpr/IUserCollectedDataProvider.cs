using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using WS.Dto;

namespace WS.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
