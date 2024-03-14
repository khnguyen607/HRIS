using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using WS.EmployeeWorkTypes.Dtos;
using WS.Dto;
using Abp.Application.Services.Dto;
using WS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using WS.Storage;

namespace WS.EmployeeWorkTypes
{
    [AbpAuthorize(AppPermissions.Pages_EmployeeWorkTypes)]
    public class EmployeeWorkTypesAppService : WSAppServiceBase, IEmployeeWorkTypesAppService
    {
        private readonly IRepository<EmployeeWorkType, long> _employeeWorkTypeRepository;

        public EmployeeWorkTypesAppService(IRepository<EmployeeWorkType, long> employeeWorkTypeRepository)
        {
            _employeeWorkTypeRepository = employeeWorkTypeRepository;

        }

        public virtual async Task<PagedResultDto<GetEmployeeWorkTypeForViewDto>> GetAll(GetAllEmployeeWorkTypesInput input)
        {

            var filteredEmployeeWorkTypes = _employeeWorkTypeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Type.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TypeFilter), e => e.Type.Contains(input.TypeFilter));

            var pagedAndFilteredEmployeeWorkTypes = filteredEmployeeWorkTypes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var employeeWorkTypes = from o in pagedAndFilteredEmployeeWorkTypes
                                    select new
                                    {

                                        o.Type,
                                        Id = o.Id
                                    };

            var totalCount = await filteredEmployeeWorkTypes.CountAsync();

            var dbList = await employeeWorkTypes.ToListAsync();
            var results = new List<GetEmployeeWorkTypeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmployeeWorkTypeForViewDto()
                {
                    EmployeeWorkType = new EmployeeWorkTypeDto
                    {

                        Type = o.Type,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmployeeWorkTypeForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_EmployeeWorkTypes_Edit)]
        public virtual async Task<GetEmployeeWorkTypeForEditOutput> GetEmployeeWorkTypeForEdit(EntityDto<long> input)
        {
            var employeeWorkType = await _employeeWorkTypeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmployeeWorkTypeForEditOutput { EmployeeWorkType = ObjectMapper.Map<CreateOrEditEmployeeWorkTypeDto>(employeeWorkType) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEmployeeWorkTypeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_EmployeeWorkTypes_Create)]
        protected virtual async Task Create(CreateOrEditEmployeeWorkTypeDto input)
        {
            var employeeWorkType = ObjectMapper.Map<EmployeeWorkType>(input);

            await _employeeWorkTypeRepository.InsertAsync(employeeWorkType);

        }

        [AbpAuthorize(AppPermissions.Pages_EmployeeWorkTypes_Edit)]
        protected virtual async Task Update(CreateOrEditEmployeeWorkTypeDto input)
        {
            var employeeWorkType = await _employeeWorkTypeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, employeeWorkType);

        }

        [AbpAuthorize(AppPermissions.Pages_EmployeeWorkTypes_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _employeeWorkTypeRepository.DeleteAsync(input.Id);
        }

    }
}