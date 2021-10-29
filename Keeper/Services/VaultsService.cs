using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsRepository;
        public VaultsService(VaultsRepository vaultsRepository)
        {
            _vaultsRepository = vaultsRepository;
        }

// THIS WILL GET ALL THE VAULTS BY THE ACCOUNT ID ---------------
        internal List<Vault> GetVaultsByAccount(string userId)
        {
            return _vaultsRepository.GetVaultsByAccount(userId);
        }
    }
}