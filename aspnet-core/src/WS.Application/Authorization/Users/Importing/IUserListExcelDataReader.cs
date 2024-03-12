using System.Collections.Generic;
using WS.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace WS.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
