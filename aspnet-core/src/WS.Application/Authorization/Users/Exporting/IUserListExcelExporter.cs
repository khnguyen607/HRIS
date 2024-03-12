using System.Collections.Generic;
using WS.Authorization.Users.Dto;
using WS.Dto;

namespace WS.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}