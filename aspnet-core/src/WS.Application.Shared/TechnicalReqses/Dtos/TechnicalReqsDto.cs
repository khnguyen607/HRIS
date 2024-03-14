using System;
using Abp.Application.Services.Dto;

namespace WS.TechnicalReqses.Dtos
{
    public class TechnicalReqsDto : EntityDto<long>
    {
        public string Value { get; set; }

    }
}