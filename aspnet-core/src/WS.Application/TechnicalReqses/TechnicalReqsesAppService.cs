using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using WS.TechnicalReqses.Exporting;
using WS.TechnicalReqses.Dtos;
using WS.Dto;
using Abp.Application.Services.Dto;
using WS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using WS.Storage;

namespace WS.TechnicalReqses
{
    [AbpAuthorize(AppPermissions.Pages_TechnicalReqses)]
    public class TechnicalReqsesAppService : WSAppServiceBase, ITechnicalReqsesAppService
    {
        private readonly IRepository<TechnicalReqs, long> _technicalReqsRepository;
        private readonly ITechnicalReqsesExcelExporter _technicalReqsesExcelExporter;

        public TechnicalReqsesAppService(IRepository<TechnicalReqs, long> technicalReqsRepository, ITechnicalReqsesExcelExporter technicalReqsesExcelExporter)
        {
            _technicalReqsRepository = technicalReqsRepository;
            _technicalReqsesExcelExporter = technicalReqsesExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetTechnicalReqsForViewDto>> GetAll(GetAllTechnicalReqsesInput input)
        {

            var filteredTechnicalReqses = _technicalReqsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.Contains(input.ValueFilter));

            var pagedAndFilteredTechnicalReqses = filteredTechnicalReqses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var technicalReqses = from o in pagedAndFilteredTechnicalReqses
                                  select new
                                  {

                                      o.Value,
                                      Id = o.Id
                                  };

            var totalCount = await filteredTechnicalReqses.CountAsync();

            var dbList = await technicalReqses.ToListAsync();
            var results = new List<GetTechnicalReqsForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTechnicalReqsForViewDto()
                {
                    TechnicalReqs = new TechnicalReqsDto
                    {

                        Value = o.Value,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTechnicalReqsForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetTechnicalReqsForViewDto> GetTechnicalReqsForView(long id)
        {
            var technicalReqs = await _technicalReqsRepository.GetAsync(id);

            var output = new GetTechnicalReqsForViewDto { TechnicalReqs = ObjectMapper.Map<TechnicalReqsDto>(technicalReqs) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TechnicalReqses_Edit)]
        public virtual async Task<GetTechnicalReqsForEditOutput> GetTechnicalReqsForEdit(EntityDto<long> input)
        {
            var technicalReqs = await _technicalReqsRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTechnicalReqsForEditOutput { TechnicalReqs = ObjectMapper.Map<CreateOrEditTechnicalReqsDto>(technicalReqs) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditTechnicalReqsDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TechnicalReqses_Create)]
        protected virtual async Task Create(CreateOrEditTechnicalReqsDto input)
        {
            var technicalReqs = ObjectMapper.Map<TechnicalReqs>(input);

            await _technicalReqsRepository.InsertAsync(technicalReqs);

        }

        [AbpAuthorize(AppPermissions.Pages_TechnicalReqses_Edit)]
        protected virtual async Task Update(CreateOrEditTechnicalReqsDto input)
        {
            var technicalReqs = await _technicalReqsRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, technicalReqs);

        }

        [AbpAuthorize(AppPermissions.Pages_TechnicalReqses_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _technicalReqsRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetTechnicalReqsesToExcel(GetAllTechnicalReqsesForExcelInput input)
        {

            var filteredTechnicalReqses = _technicalReqsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.Contains(input.ValueFilter));

            var query = (from o in filteredTechnicalReqses
                         select new GetTechnicalReqsForViewDto()
                         {
                             TechnicalReqs = new TechnicalReqsDto
                             {
                                 Value = o.Value,
                                 Id = o.Id
                             }
                         });

            var technicalReqsListDtos = await query.ToListAsync();

            return _technicalReqsesExcelExporter.ExportToFile(technicalReqsListDtos);
        }

    }
}