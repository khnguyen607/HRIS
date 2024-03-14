using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.BasicReqses.Dtos
{
    public class GetBasicReqsForEditOutput
    {
        public CreateOrEditBasicReqsDto BasicReqs { get; set; }

    }
}