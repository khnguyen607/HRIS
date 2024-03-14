using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.TechnicalReqses.Dtos
{
    public class CreateOrEditTechnicalReqsDto : EntityDto<long?>
    {

        [Required]
        public string Value { get; set; }

    }
}