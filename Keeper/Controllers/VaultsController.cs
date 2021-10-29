using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        public VaultsController(VaultsService vaultsService)
        {
            _vaultsService = vaultsService;
        }

// THIS WILL GET VALUTS BY ID ---------------------------------
        [HttpGet("{vaultsId}")]
        public ActionResult<Vault> GetById(int vaultId)
        {
            try
            {
                return Ok(_vaultsService.GetById(vaultId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL CREATE A NEW VAULT ------------------------------------
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVault.CreatorId = userInfo.Id;
                Vault createdVault = _vaultsService.Create(newVault);
                createdVault.Creator = userInfo;
                return Ok(createdVault);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

// THIS WILL UPDATE A VAULT -----------------------------------------------------------------------
        [Authorize]
        [HttpPut("{vaultId}")]
        public async Task<ActionResult<Vault>> Update(int vaultId, [FromBody] Vault updatedVault)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedVault.Id = vaultId;
                return Ok(_vaultsService.Update(updatedVault, userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL DELETE A VAULT -------------------------------------------------------------------------
        [Authorize]
        [HttpDelete("{vaultId}")]
        public async Task<ActionResult<Vault>> Delete(int vaultId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vaultsService.Delete(vaultId, userInfo.Id);
                return Ok(vault);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}