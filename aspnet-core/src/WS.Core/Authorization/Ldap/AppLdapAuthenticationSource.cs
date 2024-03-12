using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using WS.Authorization.Users;
using WS.MultiTenancy;

namespace WS.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}