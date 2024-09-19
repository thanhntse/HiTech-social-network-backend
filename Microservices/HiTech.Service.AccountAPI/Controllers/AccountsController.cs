using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HiTech.Service.AccountAPI.Data;
using HiTech.Service.AccountAPI.Entities;
using HiTech.Service.AccountAPI.Services;
using HiTech.Service.AccountAPI.DTOs.Response;
using Microsoft.AspNetCore.Http.HttpResults;
using HiTech.Shared.Controllers;
using Azure;
using HiTech.Service.AccountAPI.DTOs.Request;
using Azure.Core;

namespace HiTech.Service.AccountAPI.Controllers
{
    [Route("api/hitech/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/hitech/accounts
        [HttpGet]
        public ActionResult<ApiResponse<IAsyncEnumerable<AccountResponse>>> GetAccounts()
        {
            var accounts = _accountService.GetAllAsync();
            var response = HiTechApi.ResponseOk(accounts);
            return Ok(response);
        }

        // GET: api/hitech/accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAccount(int id)
        {
            var account = await _accountService.GetByIDAsync(id);

            var response = account != null
                ? HiTechApi.ResponseOk(account)
                : HiTechApi.ResponseNotFound<AccountResponse>();

            return account != null ? Ok(response) : NotFound(response);
        }

        // PUT: api/hitech/accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> PutAccount(int id, AccountRequest request)
        {
            var response = HiTechApi.ResponseNotFound<object>();

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.UpdateAsync(id, request);

            if (flag)
            {
                return NoContent();
            }
            response = HiTechApi.ResponseBadRequest<object>();
            return BadRequest(response);
        }

        // POST: api/hitech/accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> PostAccount(AccountRequest request)
        {
            var response = HiTechApi.ResponseConflict<AccountResponse>();
            if (await _accountService.AccountExists(request.Email))
            {
                return Conflict(response);
            }

            var account = await _accountService.CreateAsync(request);

            if (account == null)
            {
                response = HiTechApi.ResponseBadRequest<AccountResponse>();
                return BadRequest(response);
            }

            response = HiTechApi.Response(201, "Created", account);
            return CreatedAtAction("GetAccount", response);
        }

        // DELETE: api/hitech/accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAccount(int id)
        {
            var response = HiTechApi.ResponseNotFound<object>();

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.DeleteAsync(id);
            if (flag)
            {
                return NoContent();
            }
            response = HiTechApi.ResponseBadRequest<object>();
            return BadRequest(response);
        }
    }
}
