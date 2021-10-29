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
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;
        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }

//  THIS WILL GET ALL KEEPS ---------------------------------
        [HttpGet]
        public ActionResult<List<Keep>> GetAll()
        {
            try
            {
                return Ok(_keepsService.GetAll());
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL GET A KEEP BY ITS ID ---------------------------
        [HttpGet("{keepId}")]
        public ActionResult<Keep> GetById(int keepId)
        {
            try
            {
                return Ok(_keepsService.GetById(keepId));
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL CREATE A NEW KEEP ---------------------------------
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Keep>> Post([FromBody] Keep kData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                kData.CreatorId = userInfo.Id;
                Keep createKeep = _keepsService.Post(kData);
                createKeep.Creator = userInfo;
                return createKeep;
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

//  THIS WILL UPDATE A KEEP --------------------------------------
        [Authorize]
        [HttpPut("{keepId}")]
        public async Task<ActionResult<Keep>> Update(int keepId, [FromBody] Keep updatedKeep)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedKeep.Id = keepId;
                return _keepsService.Update(updatedKeep, userInfo.Id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{keepId}")]
        public async Task<ActionResult<Keep>> Delete(int keepId)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Keep keep = _keepsService.Delete(keepId, userInfo.Id);
                return Ok(keep);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}