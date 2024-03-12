using System.Collections.Generic;
using Abp;
using WS.Chat.Dto;
using WS.Dto;

namespace WS.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
