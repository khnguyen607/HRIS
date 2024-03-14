using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.TechnicalReqses.Dtos;
using WS.Dto;

namespace WS.TechnicalReqses
{
    public interface ITechnicalReqsesAppService : IApplicationService
    {
        Task<PagedResultDto<GetTechnicalReqsForViewDto>> GetAll(GetAllTechnicalReqsesInput input);

        Task<GetTechnicalReqsForViewDto> GetTechnicalReqsForView(long id);

        Task<GetTechnicalReqsForEditOutput> GetTechnicalReqsForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditTechnicalReqsDto input);

        Task Delete(EntityDto<long> input);

        Task<FileDto> GetTechnicalReqsesToExcel(GetAllTechnicalReqsesForExcelInput input);

    }
}