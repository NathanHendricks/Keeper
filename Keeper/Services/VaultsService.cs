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
        public List<Vault> GetVaultsByAccount(string profileId, string userId)
        {
            if (profileId == userId)
            {
            return  _vaultsRepository.GetVaultsByAccount(profileId);
            }
            if(profileId != userId)
            {
            return _vaultsRepository.GetVaultsByProfile(profileId);
            }
            return null;
        }

//  THIS WILL GET A VAULT BY ITS ID ---------------------------------
//  isPrivate check goes here
        public Vault GetById(int vaultId, string userId)
        {
            Vault foundVault = _vaultsRepository.GetById(vaultId);
            if(foundVault == null)
            {
                throw new System.Exception("Unable to find Vault");
            } 
            if(foundVault.CreatorId != userId)
            {
                if(foundVault.IsPrivate == true)
                {
                throw new System.Exception("Unable for you to get this vault");
                }
            }
            return foundVault;
        }

//  THIS WILL CREATE A NEW VAULT ------------------------------------
        public Vault Create(Vault newVault)
        {
            return _vaultsRepository.Create(newVault);
        }

//  THIS WILL UPDATE A VAULY BY ITS ID -----------------------------------------
        public ActionResult<Vault> Update(Vault updatedVault, string userId)
        {
            Vault foundVault = GetById(updatedVault.Id, userId);
            if(foundVault.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT TO EDIT");
            }
            foundVault.Name = updatedVault.Name ?? foundVault.Name;
            foundVault.IsPrivate = updatedVault.IsPrivate;
            foundVault.Description = updatedVault.Description ?? foundVault.Description;
            return _vaultsRepository.Update(foundVault);
        }

//  THIS WILL DELETE A VAULT --------------------------------------------
        public Vault Delete(int vaultId, string userId)
        {
            Vault foundVault = GetById(vaultId, userId);
            if(foundVault.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR VAULT");
            }
            _vaultsRepository.Delete(vaultId);
            return foundVault;
        }
    }
}