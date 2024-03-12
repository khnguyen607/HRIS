using System.Collections.Generic;
using WS.Authorization.Users.Importing.Dto;
using WS.Dto;

namespace WS.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
