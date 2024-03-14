using Abp.Application.Services.Dto;

namespace WS.Recruitments.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}