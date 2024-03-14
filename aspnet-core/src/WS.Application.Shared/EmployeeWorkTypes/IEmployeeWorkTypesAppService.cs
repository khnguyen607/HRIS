using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.EmployeeWorkTypes.Dtos;
using WS.Dto;

namespace WS.EmployeeWorkTypes
{
    public interface IEmployeeWorkTypesAppService : IApplicationService
    {
        Task<PagedResultDto<GetEmployeeWorkTypeForViewDto>> GetAll(GetAllEmployeeWorkTypesInput input);

        Task<GetEmployeeWorkTypeForEditOutput> GetEmployeeWorkTypeForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditEmployeeWorkTypeDto input);

        Task Delete(EntityDto<long> input);

    }
}