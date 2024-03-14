using System;
using Abp.Application.Services.Dto;

namespace WS.Recruitments.Dtos
{
    public class RecruitmentDto : EntityDto<long>
    {
        public string Title { get; set; }

        public string Jd { get; set; }

    }
}