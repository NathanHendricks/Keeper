using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keeper.Models;
using Keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;
        private readonly ProfilesService _profilesService;
        public ProfilesController(KeepsService keepsService, VaultsService vaultsService, ProfilesService profilesService)
        {
            _keepsService = keepsService;
            _vaultsService = vaultsService;
            _profilesService = profilesService;
        }

        // [Authorize]
        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfile(string profileId)
        {
            try
            {
                // Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_profilesService.GetById(profileId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET ALL THE VAULTS BY THE ACCOUNTS ID --------------------------------
        // [Authorize]
        [HttpGet("{profileId}/vaults")]
        public async Task<ActionResult<Vault>> GetVaultsByAccount(string profileId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vaultsService.GetVaultsByAccount(profileId, userInfo?.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET ALL THE KEEPS BY THE ACCOUNT ID --------------------------------------
        // [Authorize]
        [HttpGet("{profileId}/keeps")]
        public ActionResult<Keep> GetKeepsByAccount(string profileId)
        {
            try
            {
                // Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_keepsService.GetKeepsByAccount(profileId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}