using Abp.Application.Services.Dto;
using System;

namespace WS.Recruitments.Dtos
{
    public class GetAllRecruitmentCandidatesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string FullNameFilter { get; set; }

        public int? MaxAgeFilter { get; set; }
        public int? MinAgeFilter { get; set; }

        public string PhoneFilter { get; set; }

        public string EmailFilter { get; set; }

        public string CvFilter { get; set; }

        public int? MaxPointFilter { get; set; }
        public int? MinPointFilter { get; set; }

        public long? MaxRecruitmentIdFilter { get; set; }
        public long? MinRecruitmentIdFilter { get; set; }

        public long? RecruitmentIdFilter { get; set; }
    }
}