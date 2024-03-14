using Abp.Application.Services.Dto;
using System;

namespace WS.BasicReqses.Dtos
{
    public class GetAllBasicReqsesForExcelInput
    {
        public string Filter { get; set; }

        public string ValueFilter { get; set; }

    }
}