using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.FriendAPI.Services.IService;
using HiTech.Shared.Controllers;
using System.Security.Claims;
using HiTech.Service.FriendAPI.DTOs.Response;

namespace HiTech.Service.FriendAPI.Controllers
{
    [Route("api/hitech/friend-requests")]
    [Authorize]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly IFriendRequestService _friendRequestService;

        public FriendRequestsController(IFriendRequestService friendRequestService)
        {
            _friendRequestService = friendRequestService;
        }

        // POST: api/hitech/friend-requests/accept/5
        [HttpPost("accept/{id}")]
        public async Task<ActionResult<ApiResponse>> AcceptFriendRequests(int id)
        {
            if (!await _friendRequestService.FriendRequestExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _friendRequestService.AcceptRequest(id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // POST: api/hitech/friend-requests/5
        [HttpPost("{receiverId}")]
        public async Task<ActionResult<ApiResponse>> PostFriendRequest(int receiverId)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            var senderId = Int32.Parse(accountId);

            if (senderId == receiverId)
            {
                return BadRequest(HiTechApi.ResponseNoData(400, "Cannot request yourself."));
            }

            if (await _friendRequestService.FriendRequestExists(senderId, receiverId))
            {
                return BadRequest(HiTechApi.ResponseNoData(400, "Already request."));
            }

            bool success = await _friendRequestService.CreateAsync(senderId, receiverId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // DELETE: api/hitech/friend-requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteFriendRequest(int id)
        {
            if (!await _friendRequestService.FriendRequestExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _friendRequestService.DeleteAsync(id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // POST: api/hitech/friend-requests/deny/5
        [HttpPost("deny/{id}")]
        public async Task<ActionResult<ApiResponse>> DenyFriendRequests(int id)
        {
            if (!await _friendRequestService.FriendRequestExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _friendRequestService.DenyRequest(id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // GET: api/hitech/friend-requests/sent
        [HttpGet("sent")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FriendRequestResponse>>>> GetAllSentRequests()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var reqs = await _friendRequestService.GetAllSentRequestsAsync(Int32.Parse(accountId));
            return Ok(HiTechApi.ResponseOk(reqs));
        }

        // GET: api/hitech/friend-requests/received
        [HttpGet("received")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FriendRequestResponse>>>> GetAllReceivedRequests()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var reqs = await _friendRequestService.GetAllReceivedRequestsAsync(Int32.Parse(accountId));
            return Ok(HiTechApi.ResponseOk(reqs));
        }
    }
}
