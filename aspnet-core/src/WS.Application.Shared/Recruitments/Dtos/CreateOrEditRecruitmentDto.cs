using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.Recruitments.Dtos
{
    public class CreateOrEditRecruitmentDto : EntityDto<long?>
    {

        [Required]
        public string Title { get; set; }

        public string Jd { get; set; }

    }
}