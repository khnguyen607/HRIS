using System.Collections.Generic;
using WS.Employees.Dtos;
using WS.Dto;

namespace WS.Employees.Exporting
{
    public interface IEmployeesExcelExporter
    {
        FileDto ExportToFile(List<GetEmployeeForViewDto> employees);
    }
}