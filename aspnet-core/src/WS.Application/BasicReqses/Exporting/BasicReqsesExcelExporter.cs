using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using WS.DataExporting.Excel.NPOI;
using WS.BasicReqses.Dtos;
using WS.Dto;
using WS.Storage;

namespace WS.BasicReqses.Exporting
{
    public class BasicReqsesExcelExporter : NpoiExcelExporterBase, IBasicReqsesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public BasicReqsesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetBasicReqsForViewDto> basicReqses)
        {
            return CreateExcelPackage(
                    "BasicReqses.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("BasicReqses"));

                        AddHeader(
                            sheet,
                        L("Value")
                            );

                        AddObjects(
                            sheet, basicReqses,
                        _ => _.BasicReqs.Value
                            );

                    });

        }
    }
}