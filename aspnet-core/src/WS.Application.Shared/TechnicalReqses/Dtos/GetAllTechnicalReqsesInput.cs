using Abp.Application.Services.Dto;
using System;

namespace WS.TechnicalReqses.Dtos
{
    public class GetAllTechnicalReqsesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string ValueFilter { get; set; }

    }
}