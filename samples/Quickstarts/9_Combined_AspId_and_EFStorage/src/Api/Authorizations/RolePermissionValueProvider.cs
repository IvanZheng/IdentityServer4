using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace Api.Authorizations
{
    public class RolePermissionValueProvider : PermissionValueProvider
    {
        public const string ProviderName = "Role";

        public override string Name => ProviderName;

        public RolePermissionValueProvider(IPermissionStore permissionStore)
            : base(permissionStore)
        {

        }

        public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
        {
            var roles = context.Principal?.FindAll(JwtClaimTypes.Role)
                               .Select(c =>
                               {
                                   var role = new Role {Key = c.Value};
                                   var values = c.Value.Split(':');
                                   if (values.Length > 1)
                                   {
                                       role.Name = values[0];
                                       role.ScopeId = values[1];
                                   }
                                   else
                                   {
                                       role.Name = values[0];
                                   }

                                   return role;
                               })
                               .ToArray();
            if (!string.IsNullOrWhiteSpace(context.ScopeId))
            {
                roles = roles?.Where(r => r.ScopeId == context.ScopeId)
                              .ToArray();
            }

            if (roles == null || !roles.Any())
            {
                return PermissionGrantResult.Undefined;
            }

            foreach (var roleKey in roles.Select(r => r.Key))
            {
                if (await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, roleKey))
                {
                    return PermissionGrantResult.Granted;
                }
            }

            return PermissionGrantResult.Undefined;
        }
    }
}
