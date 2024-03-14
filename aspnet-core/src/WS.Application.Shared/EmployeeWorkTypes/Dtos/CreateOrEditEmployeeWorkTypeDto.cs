using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.EmployeeWorkTypes.Dtos
{
    public class CreateOrEditEmployeeWorkTypeDto : EntityDto<long?>
    {

        [Required]
        public string Type { get; set; }

    }
}