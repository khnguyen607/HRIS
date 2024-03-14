using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using WS.Recruitments.Dtos;
using WS.Dto;
using Abp.Application.Services.Dto;
using WS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using WS.Storage;

namespace WS.Recruitments
{
    [AbpAuthorize(AppPermissions.Pages_Recruitments)]
    public class RecruitmentsAppService : WSAppServiceBase, IRecruitmentsAppService
    {
        private readonly IRepository<Recruitment, long> _recruitmentRepository;

        public RecruitmentsAppService(IRepository<Recruitment, long> recruitmentRepository)
        {
            _recruitmentRepository = recruitmentRepository;

        }

        public virtual async Task<PagedResultDto<GetRecruitmentForViewDto>> GetAll(GetAllRecruitmentsInput input)
        {

            var filteredRecruitments = _recruitmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Title.Contains(input.Filter) || e.Jd.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title.Contains(input.TitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JdFilter), e => e.Jd.Contains(input.JdFilter));

            var pagedAndFilteredRecruitments = filteredRecruitments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var recruitments = from o in pagedAndFilteredRecruitments
                               select new
                               {

                                   o.Title,
                                   o.Jd,
                                   Id = o.Id
                               };

            var totalCount = await filteredRecruitments.CountAsync();

            var dbList = await recruitments.ToListAsync();
            var results = new List<GetRecruitmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetRecruitmentForViewDto()
                {
                    Recruitment = new RecruitmentDto
                    {

                        Title = o.Title,
                        Jd = o.Jd,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetRecruitmentForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetRecruitmentForViewDto> GetRecruitmentForView(long id)
        {
            var recruitment = await _recruitmentRepository.GetAsync(id);

            var output = new GetRecruitmentForViewDto { Recruitment = ObjectMapper.Map<RecruitmentDto>(recruitment) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Recruitments_Edit)]
        public virtual async Task<GetRecruitmentForEditOutput> GetRecruitmentForEdit(EntityDto<long> input)
        {
            var recruitment = await _recruitmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRecruitmentForEditOutput { Recruitment = ObjectMapper.Map<CreateOrEditRecruitmentDto>(recruitment) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditRecruitmentDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Recruitments_Create)]
        protected virtual async Task Create(CreateOrEditRecruitmentDto input)
        {
            var recruitment = ObjectMapper.Map<Recruitment>(input);

            await _recruitmentRepository.InsertAsync(recruitment);

        }

        [AbpAuthorize(AppPermissions.Pages_Recruitments_Edit)]
        protected virtual async Task Update(CreateOrEditRecruitmentDto input)
        {
            var recruitment = await _recruitmentRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, recruitment);

        }

        [AbpAuthorize(AppPermissions.Pages_Recruitments_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _recruitmentRepository.DeleteAsync(input.Id);
        }

    }
}