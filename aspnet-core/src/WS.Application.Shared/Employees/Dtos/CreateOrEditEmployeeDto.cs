using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.Employees.Dtos
{
    public class CreateOrEditEmployeeDto : EntityDto<long?>
    {

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Department { get; set; }

        public string JobTitle { get; set; }

        public string SocialSecurity { get; set; }

        public long? EmployeeWorkTypeId { get; set; }

    }
}