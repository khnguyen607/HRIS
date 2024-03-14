using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using WS.DataExporting.Excel.NPOI;
using WS.TechnicalReqses.Dtos;
using WS.Dto;
using WS.Storage;

namespace WS.TechnicalReqses.Exporting
{
    public class TechnicalReqsesExcelExporter : NpoiExcelExporterBase, ITechnicalReqsesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TechnicalReqsesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTechnicalReqsForViewDto> technicalReqses)
        {
            return CreateExcelPackage(
                    "TechnicalReqses.xlsx",
                    excelPackage =>
                    {

                        var sheet = excelPackage.CreateSheet(L("TechnicalReqses"));

                        AddHeader(
                            sheet,
                        L("Value")
                            );

                        AddObjects(
                            sheet, technicalReqses,
                        _ => _.TechnicalReqs.Value
                            );

                    });

        }
    }
}