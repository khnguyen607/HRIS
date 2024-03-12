using System.Collections.Generic;
using MvvmHelpers;
using WS.Models.NavigationMenu;

namespace WS.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}