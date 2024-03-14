using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.BasicReqses.Dtos
{
    public class CreateOrEditBasicReqsDto : EntityDto<long?>
    {

        public string Value { get; set; }

    }
}