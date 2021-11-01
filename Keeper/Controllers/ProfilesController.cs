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
    public class ProfilesController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        public ProfilesController(KeepsService keepsService, VaultsService vaultsService)
        {
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

//  THIS WILL GET ALL THE VAULTS BY THE ACCOUNTS ID --------------------------------
        [Authorize]
        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<Vault>> GetVaultsByAccount(string profileId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vaultsService.GetVaultsByAccount(profileId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET ALL THE KEEPS BY THE ACCOUNT ID --------------------------------------
        [Authorize]
        [HttpGet("{profileId}/keeps")]
        public async Task<ActionResult<Keep>> GetKeepsByAccount(string profileId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_keepsService.GetKeepsByAccount(profileId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}