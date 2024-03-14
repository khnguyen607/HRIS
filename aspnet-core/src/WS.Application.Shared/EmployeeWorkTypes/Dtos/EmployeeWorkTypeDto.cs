using System;
using Abp.Application.Services.Dto;

namespace WS.EmployeeWorkTypes.Dtos
{
    public class EmployeeWorkTypeDto : EntityDto<long>
    {
        public string Type { get; set; }

    }
}