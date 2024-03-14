using Abp.Application.Services.Dto;

namespace WS.TechnicalReqses.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}