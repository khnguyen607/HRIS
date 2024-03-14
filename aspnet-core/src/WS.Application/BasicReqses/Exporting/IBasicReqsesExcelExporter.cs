using System.Collections.Generic;
using WS.BasicReqses.Dtos;
using WS.Dto;

namespace WS.BasicReqses.Exporting
{
    public interface IBasicReqsesExcelExporter
    {
        FileDto ExportToFile(List<GetBasicReqsForViewDto> basicReqses);
    }
}