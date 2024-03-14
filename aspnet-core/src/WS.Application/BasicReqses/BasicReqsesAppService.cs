using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using WS.BasicReqses.Exporting;
using WS.BasicReqses.Dtos;
using WS.Dto;
using Abp.Application.Services.Dto;
using WS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using WS.Storage;

namespace WS.BasicReqses
{
    [AbpAuthorize(AppPermissions.Pages_BasicReqses)]
    public class BasicReqsesAppService : WSAppServiceBase, IBasicReqsesAppService
    {
        private readonly IRepository<BasicReqs, long> _basicReqsRepository;
        private readonly IBasicReqsesExcelExporter _basicReqsesExcelExporter;

        public BasicReqsesAppService(IRepository<BasicReqs, long> basicReqsRepository, IBasicReqsesExcelExporter basicReqsesExcelExporter)
        {
            _basicReqsRepository = basicReqsRepository;
            _basicReqsesExcelExporter = basicReqsesExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetBasicReqsForViewDto>> GetAll(GetAllBasicReqsesInput input)
        {

            var filteredBasicReqses = _basicReqsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.Contains(input.ValueFilter));

            var pagedAndFilteredBasicReqses = filteredBasicReqses
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var basicReqses = from o in pagedAndFilteredBasicReqses
                              select new
                              {

                                  o.Value,
                                  Id = o.Id
                              };

            var totalCount = await filteredBasicReqses.CountAsync();

            var dbList = await basicReqses.ToListAsync();
            var results = new List<GetBasicReqsForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBasicReqsForViewDto()
                {
                    BasicReqs = new BasicReqsDto
                    {

                        Value = o.Value,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetBasicReqsForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetBasicReqsForViewDto> GetBasicReqsForView(long id)
        {
            var basicReqs = await _basicReqsRepository.GetAsync(id);

            var output = new GetBasicReqsForViewDto { BasicReqs = ObjectMapper.Map<BasicReqsDto>(basicReqs) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_BasicReqses_Edit)]
        public virtual async Task<GetBasicReqsForEditOutput> GetBasicReqsForEdit(EntityDto<long> input)
        {
            var basicReqs = await _basicReqsRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBasicReqsForEditOutput { BasicReqs = ObjectMapper.Map<CreateOrEditBasicReqsDto>(basicReqs) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditBasicReqsDto input)
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

        [AbpAuthorize(AppPermissions.Pages_BasicReqses_Create)]
        protected virtual async Task Create(CreateOrEditBasicReqsDto input)
        {
            var basicReqs = ObjectMapper.Map<BasicReqs>(input);

            await _basicReqsRepository.InsertAsync(basicReqs);

        }

        [AbpAuthorize(AppPermissions.Pages_BasicReqses_Edit)]
        protected virtual async Task Update(CreateOrEditBasicReqsDto input)
        {
            var basicReqs = await _basicReqsRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, basicReqs);

        }

        [AbpAuthorize(AppPermissions.Pages_BasicReqses_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _basicReqsRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetBasicReqsesToExcel(GetAllBasicReqsesForExcelInput input)
        {

            var filteredBasicReqses = _basicReqsRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Value.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ValueFilter), e => e.Value.Contains(input.ValueFilter));

            var query = (from o in filteredBasicReqses
                         select new GetBasicReqsForViewDto()
                         {
                             BasicReqs = new BasicReqsDto
                             {
                                 Value = o.Value,
                                 Id = o.Id
                             }
                         });

            var basicReqsListDtos = await query.ToListAsync();

            return _basicReqsesExcelExporter.ExportToFile(basicReqsListDtos);
        }

    }
}