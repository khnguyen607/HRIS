using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WS.Recruitments.Dtos;
using WS.Dto;

namespace WS.Recruitments
{
    public interface IRecruitmentCandidatesAppService : IApplicationService
    {
        Task<PagedResultDto<GetRecruitmentCandidateForViewDto>> GetAll(GetAllRecruitmentCandidatesInput input);

        Task<GetRecruitmentCandidateForEditOutput> GetRecruitmentCandidateForEdit(EntityDto<long> input);

        Task CreateOrEdit(CreateOrEditRecruitmentCandidateDto input);

        Task Delete(EntityDto<long> input);

    }
}