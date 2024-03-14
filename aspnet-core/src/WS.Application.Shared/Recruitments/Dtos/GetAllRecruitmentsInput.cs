using Abp.Application.Services.Dto;
using System;

namespace WS.Recruitments.Dtos
{
    public class GetAllRecruitmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TitleFilter { get; set; }

        public string JdFilter { get; set; }

    }
}