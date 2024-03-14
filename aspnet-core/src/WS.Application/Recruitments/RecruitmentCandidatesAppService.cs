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
    [AbpAuthorize(AppPermissions.Pages_RecruitmentCandidates)]
    public class RecruitmentCandidatesAppService : WSAppServiceBase, IRecruitmentCandidatesAppService
    {
        private readonly IRepository<RecruitmentCandidate, long> _recruitmentCandidateRepository;

        public RecruitmentCandidatesAppService(IRepository<RecruitmentCandidate, long> recruitmentCandidateRepository)
        {
            _recruitmentCandidateRepository = recruitmentCandidateRepository;

        }

        public virtual async Task<PagedResultDto<GetRecruitmentCandidateForViewDto>> GetAll(GetAllRecruitmentCandidatesInput input)
        {

            var filteredRecruitmentCandidates = _recruitmentCandidateRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter) || e.Phone.Contains(input.Filter) || e.Email.Contains(input.Filter) || e.Cv.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName.Contains(input.FullNameFilter))
                        .WhereIf(input.MinAgeFilter != null, e => e.Age >= input.MinAgeFilter)
                        .WhereIf(input.MaxAgeFilter != null, e => e.Age <= input.MaxAgeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneFilter), e => e.Phone.Contains(input.PhoneFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email.Contains(input.EmailFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CvFilter), e => e.Cv.Contains(input.CvFilter))
                        .WhereIf(input.MinPointFilter != null, e => e.Point >= input.MinPointFilter)
                        .WhereIf(input.MaxPointFilter != null, e => e.Point <= input.MaxPointFilter)
                        .WhereIf(input.MinRecruitmentIdFilter != null, e => e.RecruitmentId >= input.MinRecruitmentIdFilter)
                        .WhereIf(input.MaxRecruitmentIdFilter != null, e => e.RecruitmentId <= input.MaxRecruitmentIdFilter)
                        .WhereIf(input.RecruitmentIdFilter.HasValue, e => false || e.RecruitmentId == input.RecruitmentIdFilter.Value);

            var pagedAndFilteredRecruitmentCandidates = filteredRecruitmentCandidates
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var recruitmentCandidates = from o in pagedAndFilteredRecruitmentCandidates
                                        select new
                                        {

                                            o.FullName,
                                            o.Age,
                                            o.Phone,
                                            o.Email,
                                            o.Cv,
                                            o.Point,
                                            o.RecruitmentId,
                                            Id = o.Id
                                        };

            var totalCount = await filteredRecruitmentCandidates.CountAsync();

            var dbList = await recruitmentCandidates.ToListAsync();
            var results = new List<GetRecruitmentCandidateForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetRecruitmentCandidateForViewDto()
                {
                    RecruitmentCandidate = new RecruitmentCandidateDto
                    {

                        FullName = o.FullName,
                        Age = o.Age,
                        Phone = o.Phone,
                        Email = o.Email,
                        Cv = o.Cv,
                        Point = o.Point,
                        RecruitmentId = o.RecruitmentId,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetRecruitmentCandidateForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_RecruitmentCandidates_Edit)]
        public virtual async Task<GetRecruitmentCandidateForEditOutput> GetRecruitmentCandidateForEdit(EntityDto<long> input)
        {
            var recruitmentCandidate = await _recruitmentCandidateRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRecruitmentCandidateForEditOutput { RecruitmentCandidate = ObjectMapper.Map<CreateOrEditRecruitmentCandidateDto>(recruitmentCandidate) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditRecruitmentCandidateDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RecruitmentCandidates_Create)]
        protected virtual async Task Create(CreateOrEditRecruitmentCandidateDto input)
        {
            var recruitmentCandidate = ObjectMapper.Map<RecruitmentCandidate>(input);

            await _recruitmentCandidateRepository.InsertAsync(recruitmentCandidate);

        }

        [AbpAuthorize(AppPermissions.Pages_RecruitmentCandidates_Edit)]
        protected virtual async Task Update(CreateOrEditRecruitmentCandidateDto input)
        {
            var recruitmentCandidate = await _recruitmentCandidateRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, recruitmentCandidate);

        }

        [AbpAuthorize(AppPermissions.Pages_RecruitmentCandidates_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _recruitmentCandidateRepository.DeleteAsync(input.Id);
        }

    }
}