using Abp.Application.Services.Dto;

namespace WS.Employees.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}