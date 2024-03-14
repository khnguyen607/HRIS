using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.Recruitments.Dtos
{
    public class GetRecruitmentForEditOutput
    {
        public CreateOrEditRecruitmentDto Recruitment { get; set; }

    }
}