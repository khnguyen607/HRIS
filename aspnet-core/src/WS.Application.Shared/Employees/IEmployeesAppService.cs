using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Employees.Dtos;
using WS.Dto;
using System.Collections.Generic;

namespace WS.Employees
{
    public interface IEmployeesAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmployeeForViewDto>> GetAll(GetAllEmployeesInput input);

        Task<GetEmployeeForViewDto> GetEmployeeForView(long id);

        Task<GetEmployeeForEditOutput> GetEmployeeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditEmployeeDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetEmployeesToExcel(GetAllEmployeesForExcelInput input);

        Task<List<EmployeeEmployeeWorkTypeLookupTableDto>> GetAllEmployeeWorkTypeForTableDropdown();

    }
}