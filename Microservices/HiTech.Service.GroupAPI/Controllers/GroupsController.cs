using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.GroupAPI.Services.IService;
using HiTech.Shared.Controllers;
using HiTech.Service.GroupAPI.DTOs.Response;
using System.Security.Claims;
using HiTech.Service.GroupAPI.DTOs.Request;

namespace HiTech.Service.GroupAPI.Controllers
{
    [Route("api/hitech/groups")]
    [Authorize]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // POST: api/hitech/groups
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GroupResponse>>> PostGroup(GroupRequest request)
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var group = await _groupService.CreateAsync(Int32.Parse(accountId), request);
            if (group == null)
            {
                return BadRequest(HiTechApi.ResponseBadRequest());
            }
            var response = HiTechApi.Response(201, "Created.", group);
            return CreatedAtAction("GetGroup",
                new { groupId = response?.Data?.GroupId }, response);
        }

        // DELETE: api/hitech/groups/5
        [HttpDelete("{groupId}")]
        public async Task<ActionResult<ApiResponse>> DeleteGroup(int groupId)
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
            var founderId = Int32.Parse(accountId);

            if (!await _groupService.IsFounder(founderId, groupId))
            {
                return Forbid();
            }

            bool success = await _groupService.DeleteAsync(groupId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // GET: api/hitech/groups
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<GroupResponse>>>> GetGroups()
        {
            var groups = await _groupService.GetAllAsync();
            return Ok(HiTechApi.ResponseOk(groups));
        }

        // GET: api/hitech/groups/5
        [HttpGet("{groupId}")]
        public async Task<ActionResult<ApiResponse<GroupResponse>>> GetGroup(int groupId)
        {
            var group = await _groupService.GetByIDAsync(groupId);
            if (group == null)
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }
            return Ok(HiTechApi.ResponseOk(group));
        }

        // GET: api/hitech/groups/of-user
        [HttpGet("of-user")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GroupResponse>>>> GetGroupsOfUser()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var groups = await _groupService.GetAllByUserIDAsync(Int32.Parse(accountId));
            return Ok(HiTechApi.ResponseOk(groups));
        }

        // GET: api/hitech/groups/all-user
        [HttpGet("all-user/{groupId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponse>>>> GetAllUserOfGroup(int groupId)
        {
            var users = await _groupService.GetAllUserByGroupIDAsync(groupId);
            return Ok(HiTechApi.ResponseOk(users));
        }

        // DELETE: api/hitech/groups/kick-user/5
        [HttpDelete("kick-user/{groupId}")]
        public async Task<ActionResult<ApiResponse>> KickUser(int groupId, int userId)
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
            var founderId = Int32.Parse(accountId);

            if (!await _groupService.IsFounder(founderId, groupId))
            {
                return Forbid();
            }

            if (!await _groupService.UserExistsInGroup(userId, groupId))
            {
                return NotFound(HiTechApi.ResponseNoData(400, "User not exist in group"));
            }

            bool success = await _groupService.KickUser(groupId, userId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // DELETE: api/hitech/groups/out/5
        [HttpDelete("out/{groupId}")]
        public async Task<ActionResult<ApiResponse>> OutGroup(int groupId)
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
            var userId = Int32.Parse(accountId);

            if (!await _groupService.UserExistsInGroup(userId, groupId))
            {
                return NotFound(HiTechApi.ResponseNoData(400, "User not exist in group"));
            }

            bool success = await _groupService.OutGroup(groupId, userId);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // PUT: api/hitech/groups/5
        [HttpPut("{groupId}")]
        public async Task<ActionResult<ApiResponse>> PutGroup(int groupId, GroupRequest request)
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
            var founderId = Int32.Parse(accountId);

            if (!await _groupService.IsFounder(founderId, groupId))
            {
                return Forbid();
            }

            bool success = await _groupService.UpdateAsync(groupId, request);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
