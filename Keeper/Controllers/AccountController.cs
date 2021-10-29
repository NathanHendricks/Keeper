using System;
using System.Threading.Tasks;
using Keeper.Models;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Keeper.Services;

namespace Keeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public AccountController(AccountService accountService, KeepsService keepsService, VaultsService vaultsService)
        {
            _accountService = accountService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        // THIS GETS THE ACCOUNT OR IT WILL CREATE A NEW ONE ----------------------------
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET ALL THE VAULTS BY THE ACCOUNTS ID --------------------------------
        [Authorize]
        [HttpGet("{vaults}")]
        public async Task<ActionResult<Vault>> GetVaultsByAccount()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_vaultsService.GetVaultsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET ALL THE KEEPS BY THE ACCOUNT ID --------------------------------------
        [Authorize]
        [HttpGet("{keeps}")]
        public async Task<ActionResult<Keep>> GetKeepsByAccount()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_keepsService.GetKeepsByAccount(userInfo.Id));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}