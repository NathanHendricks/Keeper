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
    public class VaultkeepsController : ControllerBase
    {
        private readonly VaultkeepsService _vaultkeepsService;
        public VaultkeepsController(VaultkeepsService vaultkeepsService)
        {
            _vaultkeepsService = vaultkeepsService;
        }

//  THIS WILL GET A VAULTKEEP BY ITS ID ------------------------------------------
        [HttpGet("{vaultKeepId}")]
        public ActionResult<VaultkeepViewModel> GetById(int vaultkeepId)
        {
            try
            {
                return Ok(_vaultkeepsService.GetById(vaultkeepId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

// THIS WILL CREATE A NEW VAULTKEEP --------------------------------------------------------
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Vaultkeep>> Create([FromBody] Vaultkeep newVaultKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                newVaultKeep.CreatorId = userInfo.Id;
                Vaultkeep createVK = _vaultkeepsService.Create(newVaultKeep);
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
        public async Task<ActionResult<VaultkeepViewModel>> Delete(int vaultKeepId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                VaultkeepViewModel VK = _vaultkeepsService.Delete(vaultKeepId, userInfo.Id);
                return Ok(VK);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}