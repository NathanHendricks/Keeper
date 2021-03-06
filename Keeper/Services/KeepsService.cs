using System.Collections.Generic;
using Keeper.Models;
using Keeper.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsRepository;
        public KeepsService(KeepsRepository keepsRepository)
        {
            _keepsRepository = keepsRepository;
        }

//  THIS WILL GET ALL KEEPS BY THE ACCOUNT ID -----------------
        public List<Keep> GetKeepsByAccount(string profileId)
        {
            return _keepsRepository.GetKeepsByAccount(profileId);
        }

//  THIS WILL GET ALL THE KEEPS ----------------------------------
        public List<Keep> GetAll()
        {
            return _keepsRepository.GetAll();
        }

//  THIS WILL GET A KEEP BY ITS ID --------------------------------
        public Keep GetById(int keepId)
        {
            Keep foundkeep = _keepsRepository.GetById(keepId);
            if(foundkeep == null)
            {
                throw new System.Exception("Unable to find Keep");
            }
            return foundkeep;
        }

//  THIS WILL CREATE A NEW KEEP ---------------------------------
        public Keep Create(Keep newData)
        {
            return _keepsRepository.Create(newData);
        }

//  THIS WILL UPDATE A KEEP --------------------------------------------------
        public ActionResult<Keep> Update(Keep updatedKeep, string userId)
        {
            Keep foundKeep = GetById(updatedKeep.Id);
            if(foundKeep.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR KEEP");
            }
            foundKeep.Name = updatedKeep.Name ?? foundKeep.Name;
            foundKeep.Description = updatedKeep.Description ?? foundKeep.Description;
            foundKeep.Img = updatedKeep.Img ?? foundKeep.Img;
            return _keepsRepository.Update(foundKeep);
        }

//  THIS WILL DELETE A KEEP -----------------------------------
        public Keep Delete(int keepId, string userId)
        {
            Keep foundkeep = GetById(keepId);
            if(foundkeep.CreatorId != userId)
            {
                throw new System.Exception("THIS IS NOT YOUR KEEP");
            }
            _keepsRepository.Delete(keepId);
            return foundkeep;
        }
    }
}