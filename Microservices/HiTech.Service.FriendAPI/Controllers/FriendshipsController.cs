using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.FriendAPI.Services.IService;
using HiTech.Shared.Controllers;
using System.Security.Claims;
using HiTech.Service.FriendAPI.DTOs.Response;

namespace HiTech.Service.FriendAPI.Controllers
{
    [Route("api/hitech/friendships")]
    [Authorize]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipsController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        // DELETE: api/hitech/friendships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> UnFriend(int id)
        {
            if (!await _friendshipService.FriendshipExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            bool success = await _friendshipService.DeleteAsync(Int32.Parse(accountId), id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // GET: api/hitech/friendships/user
        [HttpGet("user")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponse>>>> GetFriends()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var friends = await _friendshipService.GetAllFriendAsync(Int32.Parse(accountId));
            return Ok(HiTechApi.ResponseOk(friends));
        }

        // GET: api/hitech/friendships/user/detail
        [HttpGet("user/detail")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FriendshipResponse>>>> GetDetailFriends()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var friends = await _friendshipService.GetAllFriendDetailAsync(Int32.Parse(accountId));
            return Ok(HiTechApi.ResponseOk(friends));
        }

        // PUT: api/hitech/friendships/toggle-block/5
        [HttpPut("toggle-block/{id}")]
        public async Task<ActionResult<ApiResponse>> BlockFriend(int id)
        {
            if (!await _friendshipService.FriendshipExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            bool success = await _friendshipService.ToggleBlockAsync(Int32.Parse(accountId), id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
