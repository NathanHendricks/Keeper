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
        public List<VaultkeepViewModel> GetKeepVaults(int vaultId)
        { 
            return _vaultkeepsRepository.GetKeepVaults(vaultId);
        }

//  THIS WILL GET A VAULTKEEP BY ITS ID --------------------------------------------
        public VaultkeepViewModel GetById(int vaultkeepId)
        {
            VaultkeepViewModel foundVK = _vaultkeepsRepository.GetById(vaultkeepId);
            if (foundVK == null)
            {
                throw new System.Exception("CAN NOT FIND THAT ID");
            }
            return foundVK;
        }

//  THIS WILL CREATE A NEW VAULTKEEP --------------------------------------------
        public Vaultkeep Create(Vaultkeep newVK)
        {
            return _vaultkeepsRepository.Create(newVK);
        }

// THIS WILL DELETE A VAULTKEEP -------------------------------------------------
        public VaultkeepViewModel Delete(int vaultkeepId, string userId)
        {
            VaultkeepViewModel foundVK = GetById(vaultkeepId);
            if(foundVK.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT KEEP");
            }
            _vaultkeepsRepository.Delete(vaultkeepId);
            return foundVK;
        }
    }
}