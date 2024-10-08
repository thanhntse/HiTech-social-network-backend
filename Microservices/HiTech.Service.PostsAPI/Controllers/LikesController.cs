﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Shared.Controllers;
using System.Security.Claims;
using HiTech.Service.PostsAPI.DTOs.Response;

namespace HiTech.Service.PostsAPI.Controllers
{
    [Route("api/hitech/likes")]
    [Authorize]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IPostService _postService;

        public LikesController(ILikeService likeService, IPostService postService)
        {
            _likeService = likeService;
            _postService = postService;
        }

        // GET: api/hitech/likes/post/5
        [HttpGet("post/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponse>>>> GetAllAuthorIDByPostID(int id)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var authors = await _likeService.GetAllUserByPostIDAsync(id);
            return Ok(HiTechApi.ResponseOk(authors));
        }

        // POST: api/hitech/likes/post/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post/{id}")]
        public async Task<ActionResult<ApiResponse>> PostLike(int id)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            if (await _likeService.LikeExists(accountId, id))
            {
                return BadRequest(HiTechApi.ResponseNoData(400, "Already like."));
            }

            bool success = await _likeService.CreateAsync(accountId, id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // DELETE: api/hitech/likes/post/5
        [HttpDelete("post/{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteLike(int id)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (accountId == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            if (!await _likeService.LikeExists(accountId, id))
            {
                return BadRequest(HiTechApi.ResponseNoData(400, "Didn't like yet."));
            }

            bool success = await _likeService.DeleteAsync(accountId, id);
            if (success)
            {
                return Ok(HiTechApi.ResponseOk());
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
