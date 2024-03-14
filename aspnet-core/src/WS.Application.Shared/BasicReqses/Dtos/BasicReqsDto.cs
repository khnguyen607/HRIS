using System;
using Abp.Application.Services.Dto;

namespace WS.BasicReqses.Dtos
{
    public class BasicReqsDto : EntityDto<long>
    {
        public string Value { get; set; }

    }
}