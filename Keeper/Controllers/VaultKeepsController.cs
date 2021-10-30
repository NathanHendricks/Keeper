using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("{api/[controller]}")]
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;
        public VaultKeepsController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }

// THIS WILL GET ALL VAULTKEEPS -----------------------------------------------
        [HttpGet("{vaultId}/keeps")]
        public ActionResult<List<VaultKeep>> GetKeepVaults(int vaultId)
        {
            try
            {
                List<VaultKeep> VK = _vaultKeepsService.GetKeepVaults(vaultId);
                return Ok(VK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        } 

//  THIS WILL GET A VAULTKEEP BY ITS ID ------------------------------------------
        [HttpGet("{vaultKeepId}")]
        public ActionResult<VaultKeep> GetById(int vaultKeepId)
        {
            try
            {
                return Ok(_vaultKeepsService.GetById(vaultKeepId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

// THIS WILL CREATE A NEW VAULTKEEP --------------------------------------------------------
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVaultKeep.CreatorId = userInfo.Id;
                VaultKeep createVK = _vaultKeepsService.Create(newVaultKeep);
                createVK.Creator = userInfo;
                return Ok(createVK);
                
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

// THIS WILL DELETE A VAULT KEEP -----------------------------------------------------
        [Authorize]
        [HttpDelete("{vaultKeepId}")]
        public async Task<ActionResult<VaultKeep>> Delete(int vaultKeepId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                VaultKeep VK = _vaultKeepsService.Delete(vaultKeepId, userInfo.Id);
                return Ok(VK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}