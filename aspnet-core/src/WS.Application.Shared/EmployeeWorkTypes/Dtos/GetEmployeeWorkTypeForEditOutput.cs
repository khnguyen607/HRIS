using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.EmployeeWorkTypes.Dtos
{
    public class GetEmployeeWorkTypeForEditOutput
    {
        public CreateOrEditEmployeeWorkTypeDto EmployeeWorkType { get; set; }

    }
}