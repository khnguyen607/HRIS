using Abp.Application.Services.Dto;

namespace WS.EmployeeWorkTypes.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}