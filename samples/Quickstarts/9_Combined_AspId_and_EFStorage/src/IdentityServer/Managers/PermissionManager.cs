using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServerAspNetIdentity.Data;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Managers
{
    public class PermissionManager
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> IsGrantedAsync(string name,
                                         string providerType,
                                         string providerKey)
        {
            return _dbContext.PermissionGrants.AnyAsync(pg => pg.Name == name && 
                                                              pg.ProviderType == providerType &&
                                                              pg.ProviderKey == providerKey);
        }
    }
}
