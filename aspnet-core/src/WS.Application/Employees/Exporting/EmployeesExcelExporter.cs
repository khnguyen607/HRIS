using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using WS.DataExporting.Excel.NPOI;
using WS.Employees.Dtos;
using WS.Dto;
using WS.Storage;

namespace WS.Employees.Exporting
{
    public class EmployeesExcelExporter : NpoiExcelExporterBase, IEmployeesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EmployeesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEmployeeForViewDto> employees)
        {
            return CreateExcelPackage(
                    "Employees.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("Employees"));

                        AddHeader(
                            sheet,
                        L("FullName"),
                        L("Address"),
                        L("Phone"),
                        L("Department"),
                        L("JobTitle"),
                        L("SocialSecurity"),
                        (L("EmployeeWorkType")) + L("Type")
                            );

                        AddObjects(
                            sheet, employees,
                        _ => _.Employee.FullName,
                        _ => _.Employee.Address,
                        _ => _.Employee.Phone,
                        _ => _.Employee.Department,
                        _ => _.Employee.JobTitle,
                        _ => _.Employee.SocialSecurity,
                        _ => _.EmployeeWorkTypeType
                            );

                    });

        }
    }
}