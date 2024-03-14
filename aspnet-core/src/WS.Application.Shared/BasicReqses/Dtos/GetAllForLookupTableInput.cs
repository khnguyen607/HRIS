using Abp.Application.Services.Dto;

namespace WS.BasicReqses.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}