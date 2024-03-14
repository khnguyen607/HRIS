using Abp.Application.Services.Dto;
using System;

namespace WS.EmployeeWorkTypes.Dtos
{
    public class GetAllEmployeeWorkTypesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string TypeFilter { get; set; }

    }
}