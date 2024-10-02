using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.GroupAPI.Services.IService;
using HiTech.Shared.Controllers;
using System.Security.Claims;
using HiTech.Service.GroupAPI.DTOs.Response;

namespace HiTech.Service.GroupAPI.Controllers
{
    [Route("api/hitech/join-requests")]
    [Authorize]
    [ApiController]
    public class JoinRequestsController : ControllerBase
    {
        private readonly IJoinRequestService _joinRequestService;
        private readonly IGroupService _groupService;

        public JoinRequestsController(IJoinRequestService joinRequestService, IGroupService groupService)
        {
            _joinRequestService = joinRequestService;
            _groupService = groupService;
        }

        // PUT: api/hitech/join-requests/accept/5
        [HttpPut("accept/{reqId}")]
        public async Task<ActionResult<ApiResponse>> AcceptJoinRequests(int reqId)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            var founderId = Int32.Parse(accountId);

            if (!await _joinRequestService.JoinRequestExists(reqId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _joinRequestService.AcceptRequest(founderId, reqId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // POST: api/hitech/join-requests/5
        [HttpPost("{groupId}")]
        public async Task<ActionResult<ApiResponse>> PostJoinRequest(int groupId)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            var userId = Int32.Parse(accountId);

            if (await _groupService.UserExistsInGroup(userId, groupId) ||
                await _joinRequestService.JoinRequestExists(userId, groupId))
            {
                return BadRequest(HiTechApi.ResponseNoData(400, "Already request."));
            }

            if (!await _groupService.GroupExists(groupId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _joinRequestService.CreateAsync(userId, groupId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // DELETE: api/hitech/join-requests/5
        [HttpDelete("{reqId}")]
        public async Task<ActionResult<ApiResponse>> DeleteJoinRequest(int reqId)
        {
            if (!await _joinRequestService.JoinRequestExists(reqId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            bool success = await _joinRequestService.DeleteAsync(Int32.Parse(accountId), reqId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // PUT: api/hitech/join-requests/deny/5
        [HttpPut("deny/{reqId}")]
        public async Task<ActionResult<ApiResponse>> DenyJoinRequests(int reqId)
        {
            if (!await _joinRequestService.JoinRequestExists(reqId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            bool success = await _joinRequestService.DenyRequest(Int32.Parse(accountId), reqId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // GET: api/hitech/join-requests/group/all/5
        [HttpGet("group/all/{groupId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<JoinRequestResponse>>>> GetJoinRequests(int groupId)
        {
            if (!await _groupService.GroupExists(groupId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            var fouderId = Int32.Parse(accountId);

            if (!await _groupService.IsFounder(fouderId, groupId))
            {
                return Forbid();
            }

            var reqs = await _joinRequestService.GetAllByGroupIDAsync(groupId);
            return Ok(HiTechApi.ResponseOk(reqs));
        }

        // GET: api/hitech/join-requests/group/pending/5
        [HttpGet("group/pending/{groupId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<JoinRequestResponse>>>> GetPendingJoinRequests(int groupId)
        {
            if (!await _groupService.GroupExists(groupId))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }
            var fouderId = Int32.Parse(accountId);

            if (!await _groupService.IsFounder(fouderId, groupId))
            {
                return Forbid();
            }

            var reqs = await _joinRequestService.GetAllPendingRequestByGroupIDAsync(groupId);
            return Ok(HiTechApi.ResponseOk(reqs));
        }
    }
}
