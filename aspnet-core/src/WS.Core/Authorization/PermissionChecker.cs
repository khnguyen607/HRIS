using Abp.Authorization;
using WS.Authorization.Roles;
using WS.Authorization.Users;

namespace WS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
