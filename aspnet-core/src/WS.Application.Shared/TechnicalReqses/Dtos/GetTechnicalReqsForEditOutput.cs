using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.TechnicalReqses.Dtos
{
    public class GetTechnicalReqsForEditOutput
    {
        public CreateOrEditTechnicalReqsDto TechnicalReqs { get; set; }

    }
}