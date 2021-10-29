using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;
using Microsoft.AspNetCore.Mvc;

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

//  THIS WILL GET A VAULT BY ITS ID ---------------------------------
        internal Vault GetById(int vaultId)
        {
            Vault foundVault = _vaultsRepository.GetById(vaultId);
            if(foundVault == null)
            {
                throw new System.Exception("Unable to find Vault");
            } 
            return foundVault;
        }

//  THIS WILL CREATE A NEW VAULT ------------------------------------
        internal Vault Create(Vault newVault)
        {
            return _vaultsRepository.Create(newVault);
        }

//  THIS WILL GET A VAULY BY ITS ID -----------------------------------------
        internal ActionResult<Vault> Update(Vault updatedVault, string userId)
        {
            Vault foundVault = GetById(updatedVault.Id);
            if(foundVault.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT");
            }
            foundVault.Name = updatedVault.Name ?? foundVault.Name;
            foundVault.IsPrivate = updatedVault.IsPrivate;
            foundVault.Description = updatedVault.Description ?? foundVault.Description;
            return _vaultsRepository.Update(foundVault);
        }

        internal Vault Delete(int vaultId, string userId)
        {
            Vault foundVault = GetById(vaultId);
            if(foundVault.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT");
            }
            _vaultsRepository.Delete(vaultId);
            return foundVault;
        }
    }
}