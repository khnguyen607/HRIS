using WS.EmployeeWorkTypes;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using WS.Employees.Exporting;
using WS.Employees.Dtos;
using WS.Dto;
using Abp.Application.Services.Dto;
using WS.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using WS.Storage;

namespace WS.Employees
{
    [AbpAuthorize(AppPermissions.Pages_Employees)]
    public class EmployeesAppService : WSAppServiceBase, IEmployeesAppService
    {
        private readonly IRepository<Employee, long> _employeeRepository;
        private readonly IEmployeesExcelExporter _employeesExcelExporter;
        private readonly IRepository<EmployeeWorkType, long> _lookup_employeeWorkTypeRepository;

        public EmployeesAppService(IRepository<Employee, long> employeeRepository, IEmployeesExcelExporter employeesExcelExporter, IRepository<EmployeeWorkType, long> lookup_employeeWorkTypeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeesExcelExporter = employeesExcelExporter;
            _lookup_employeeWorkTypeRepository = lookup_employeeWorkTypeRepository;

        }

        public virtual async Task<PagedResultDto<GetEmployeeForViewDto>> GetAll(GetAllEmployeesInput input)
        {

            var filteredEmployees = _employeeRepository.GetAll()
                        .Include(e => e.EmployeeWorkTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.Phone.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.JobTitle.Contains(input.Filter) || e.SocialSecurity.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName.Contains(input.FullNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address.Contains(input.AddressFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneFilter), e => e.Phone.Contains(input.PhoneFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department.Contains(input.DepartmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobTitleFilter), e => e.JobTitle.Contains(input.JobTitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SocialSecurityFilter), e => e.SocialSecurity.Contains(input.SocialSecurityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmployeeWorkTypeTypeFilter), e => e.EmployeeWorkTypeFk != null && e.EmployeeWorkTypeFk.Type == input.EmployeeWorkTypeTypeFilter);

            var pagedAndFilteredEmployees = filteredEmployees
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var employees = from o in pagedAndFilteredEmployees
                            join o1 in _lookup_employeeWorkTypeRepository.GetAll() on o.EmployeeWorkTypeId equals o1.Id into j1
                            from s1 in j1.DefaultIfEmpty()

                            select new
                            {

                                o.FullName,
                                o.Address,
                                o.Phone,
                                o.Department,
                                o.JobTitle,
                                o.SocialSecurity,
                                Id = o.Id,
                                EmployeeWorkTypeType = s1 == null || s1.Type == null ? "" : s1.Type.ToString()
                            };

            var totalCount = await filteredEmployees.CountAsync();

            var dbList = await employees.ToListAsync();
            var results = new List<GetEmployeeForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEmployeeForViewDto()
                {
                    Employee = new EmployeeDto
                    {

                        FullName = o.FullName,
                        Address = o.Address,
                        Phone = o.Phone,
                        Department = o.Department,
                        JobTitle = o.JobTitle,
                        SocialSecurity = o.SocialSecurity,
                        Id = o.Id,
                    },
                    EmployeeWorkTypeType = o.EmployeeWorkTypeType
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEmployeeForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEmployeeForViewDto> GetEmployeeForView(long id)
        {
            var employee = await _employeeRepository.GetAsync(id);

            var output = new GetEmployeeForViewDto { Employee = ObjectMapper.Map<EmployeeDto>(employee) };

            if (output.Employee.EmployeeWorkTypeId != null)
            {
                var _lookupEmployeeWorkType = await _lookup_employeeWorkTypeRepository.FirstOrDefaultAsync((long)output.Employee.EmployeeWorkTypeId);
                output.EmployeeWorkTypeType = _lookupEmployeeWorkType?.Type?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Employees_Edit)]
        public virtual async Task<GetEmployeeForEditOutput> GetEmployeeForEdit(EntityDto<long> input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEmployeeForEditOutput { Employee = ObjectMapper.Map<CreateOrEditEmployeeDto>(employee) };

            if (output.Employee.EmployeeWorkTypeId != null)
            {
                var _lookupEmployeeWorkType = await _lookup_employeeWorkTypeRepository.FirstOrDefaultAsync((long)output.Employee.EmployeeWorkTypeId);
                output.EmployeeWorkTypeType = _lookupEmployeeWorkType?.Type?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEmployeeDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Employees_Create)]
        protected virtual async Task Create(CreateOrEditEmployeeDto input)
        {
            var employee = ObjectMapper.Map<Employee>(input);

            await _employeeRepository.InsertAsync(employee);

        }

        [AbpAuthorize(AppPermissions.Pages_Employees_Edit)]
        protected virtual async Task Update(CreateOrEditEmployeeDto input)
        {
            var employee = await _employeeRepository.FirstOrDefaultAsync((long)input.Id);
            ObjectMapper.Map(input, employee);

        }

        [AbpAuthorize(AppPermissions.Pages_Employees_Delete)]
        public virtual async Task Delete(EntityDto<long> input)
        {
            await _employeeRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetEmployeesToExcel(GetAllEmployeesForExcelInput input)
        {

            var filteredEmployees = _employeeRepository.GetAll()
                        .Include(e => e.EmployeeWorkTypeFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter) || e.Address.Contains(input.Filter) || e.Phone.Contains(input.Filter) || e.Department.Contains(input.Filter) || e.JobTitle.Contains(input.Filter) || e.SocialSecurity.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName.Contains(input.FullNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address.Contains(input.AddressFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneFilter), e => e.Phone.Contains(input.PhoneFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department.Contains(input.DepartmentFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobTitleFilter), e => e.JobTitle.Contains(input.JobTitleFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SocialSecurityFilter), e => e.SocialSecurity.Contains(input.SocialSecurityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmployeeWorkTypeTypeFilter), e => e.EmployeeWorkTypeFk != null && e.EmployeeWorkTypeFk.Type == input.EmployeeWorkTypeTypeFilter);

            var query = (from o in filteredEmployees
                         join o1 in _lookup_employeeWorkTypeRepository.GetAll() on o.EmployeeWorkTypeId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         select new GetEmployeeForViewDto()
                         {
                             Employee = new EmployeeDto
                             {
                                 FullName = o.FullName,
                                 Address = o.Address,
                                 Phone = o.Phone,
                                 Department = o.Department,
                                 JobTitle = o.JobTitle,
                                 SocialSecurity = o.SocialSecurity,
                                 Id = o.Id
                             },
                             EmployeeWorkTypeType = s1 == null || s1.Type == null ? "" : s1.Type.ToString()
                         });

            var employeeListDtos = await query.ToListAsync();

            return _employeesExcelExporter.ExportToFile(employeeListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Employees)]
        public async Task<List<EmployeeEmployeeWorkTypeLookupTableDto>> GetAllEmployeeWorkTypeForTableDropdown()
        {
            return await _lookup_employeeWorkTypeRepository.GetAll()
                .Select(employeeWorkType => new EmployeeEmployeeWorkTypeLookupTableDto
                {
                    Id = employeeWorkType.Id,
                    DisplayName = employeeWorkType == null || employeeWorkType.Type == null ? "" : employeeWorkType.Type.ToString()
                }).ToListAsync();
        }

    }
}