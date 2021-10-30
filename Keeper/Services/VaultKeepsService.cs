using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;

namespace Keeper.Services
{
    public class VaultkeepsService
    {
        private readonly VaultkeepsRepository _vaultkeepsRepository;
        public VaultkeepsService(VaultkeepsRepository vaultkeepsRepository)
        {
            _vaultkeepsRepository = vaultkeepsRepository;
        }

// THIS WILL GET ALL VAULTKEEPS ----------------------------------------------------
        internal List<VaultKeep> GetKeepVaults(int vaultId)
        { 
            return _vaultkeepsRepository.GetKeepVaults(vaultId);
        }

//  THIS WILL GET A VAULTKEEP BY ITS ID --------------------------------------------
        internal VaultKeep GetById(int vaultKeepId)
        {
            VaultKeep foundVK = _vaultkeepsRepository.GetById(vaultKeepId);
            if(foundVK == null)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULTKEEP");
            }
            return foundVK;
        }

//  THIS WILL CREATE A NEW VAULTKEEP --------------------------------------------
        internal VaultKeep Create(VaultKeep newVK)
        {
            return _vaultkeepsRepository.Create(newVK);
        }

// THIS WILL DELETE A VAULTKEEP -------------------------------------------------
        internal VaultKeep Delete(int vaultKeepId, string userId)
        {
            VaultKeep foundVK = GetById(vaultKeepId);
            if(foundVK.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT KEEP");
            }
            _vaultkeepsRepository.Delete(vaultKeepId);
            return foundVK;
        }
    }
}