﻿using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Identity;
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
                                         string providerKey,
                                         string tenantId)
        {
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                tenantId = null;
            }

            return _dbContext.PermissionGrants.AnyAsync(pg => pg.Name == name &&
                                                              pg.ProviderType == providerType &&
                                                              pg.ProviderKey == providerKey &&
                                                              pg.TenantId == tenantId);
        }

        public async Task<IdentityResult> GrantAsync(string name,
                                                     string providerType,
                                                     string providerKey,
                                                     string tenantId)
        {
            if (string.IsNullOrWhiteSpace(tenantId))
            {
                tenantId = null;
            }

            if (!await _dbContext.PermissionGrants
                                 .AnyAsync(pg => pg.Name == name &&
                                                 pg.ProviderType == providerType &&
                                                 pg.ProviderKey == providerKey &&
                                                 pg.TenantId == tenantId)
                                 .ConfigureAwait(false))
            {
                _dbContext.PermissionGrants.Add(new ApplicationPermissionGrant(name, providerKey, providerType, tenantId));
                await _dbContext.SaveChangesAsync();
            }

            return IdentityResult.Success;
        }
    }
}