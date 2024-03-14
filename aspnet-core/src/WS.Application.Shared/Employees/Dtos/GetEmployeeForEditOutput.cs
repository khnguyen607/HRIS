using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.Employees.Dtos
{
    public class GetEmployeeForEditOutput
    {
        public CreateOrEditEmployeeDto Employee { get; set; }

        public string EmployeeWorkTypeType { get; set; }

    }
}