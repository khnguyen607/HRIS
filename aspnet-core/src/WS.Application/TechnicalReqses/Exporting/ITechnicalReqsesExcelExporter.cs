using System.Collections.Generic;
using WS.TechnicalReqses.Dtos;
using WS.Dto;

namespace WS.TechnicalReqses.Exporting
{
    public interface ITechnicalReqsesExcelExporter
    {
        FileDto ExportToFile(List<GetTechnicalReqsForViewDto> technicalReqses);
    }
}