using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace WS.Recruitments.Dtos
{
    public class GetRecruitmentCandidateForEditOutput
    {
        public CreateOrEditRecruitmentCandidateDto RecruitmentCandidate { get; set; }

    }
}