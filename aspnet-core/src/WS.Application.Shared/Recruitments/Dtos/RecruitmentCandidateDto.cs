using System;
using Abp.Application.Services.Dto;

namespace WS.Recruitments.Dtos
{
    public class RecruitmentCandidateDto : EntityDto<long>
    {
        public string FullName { get; set; }

        public int? Age { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Cv { get; set; }

        public int Point { get; set; }

        public long RecruitmentId { get; set; }

    }
}