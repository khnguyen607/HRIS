using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.BasicReqses.Dtos;
using WS.Dto;

namespace WS.BasicReqses
{
    public interface IBasicReqsesAppService : IApplicationService
    {
        Task<PagedResultDto<GetBasicReqsForViewDto>> GetAll(GetAllBasicReqsesInput input);

        Task<GetBasicReqsForViewDto> GetBasicReqsForView(long id);

        Task<GetBasicReqsForEditOutput> GetBasicReqsForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditBasicReqsDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetBasicReqsesToExcel(GetAllBasicReqsesForExcelInput input);

    }
}