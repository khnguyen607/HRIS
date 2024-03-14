using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Recruitments.Dtos;
using WS.Dto;

namespace WS.Recruitments
{
    public interface IRecruitmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetRecruitmentForViewDto>> GetAll(GetAllRecruitmentsInput input);

        Task<GetRecruitmentForViewDto> GetRecruitmentForView(long id);

        Task<GetRecruitmentForEditOutput> GetRecruitmentForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditRecruitmentDto input);

        Task Delete(EntityDto<long> input);

    }
}