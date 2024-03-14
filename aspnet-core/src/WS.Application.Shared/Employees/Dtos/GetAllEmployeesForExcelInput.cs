using Abp.Application.Services.Dto;
using System;

namespace WS.Employees.Dtos
{
    public class GetAllEmployeesForExcelInput
    {
        public string Filter { get; set; }

        public string FullNameFilter { get; set; }

        public string AddressFilter { get; set; }

        public string PhoneFilter { get; set; }

        public string DepartmentFilter { get; set; }

        public string JobTitleFilter { get; set; }

        public string SocialSecurityFilter { get; set; }

        public string EmployeeWorkTypeTypeFilter { get; set; }

    }
}