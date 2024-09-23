using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Shared.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiTech.Service.AuthAPI.Controllers
{
    [Route("api/hitech/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/hitech/account
        [Authorize]
        [HttpGet]
        public ActionResult<ApiResponse<IAsyncEnumerable<AccountResponse>>> GetAccounts()
        {
            var accounts = _accountService.GetAllAsync();
            var response = HiTechApi.ResponseOk(accounts);
            return Ok(response);
        }

        // GET: api/hitech/account/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> GetAccount(int id)
        {
            var account = await _accountService.GetByIDAsync(id);

            var response = account != null
                ? HiTechApi.ResponseOk(account)
                : HiTechApi.ResponseNotFound();

            return account != null ? Ok(response) : NotFound(response);
        }

        // PUT: api/hitech/account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutAccount(int id, AccountUpdationRequest request)
        {
            var response = HiTechApi.ResponseNotFound();

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.UpdateAsync(id, request);

            if (flag)
            {
                return NoContent();
            }
            response = HiTechApi.ResponseBadRequest();
            return BadRequest(response);
        }

        // POST: api/hitech/account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AccountResponse>>> PostAccount(AccountCreationRequest request)
        {
            var response = HiTechApi.ResponseConflict();
            if (await _accountService.AccountExists(request.Email))
            {
                return Conflict(response);
            }

            var account = await _accountService.CreateAsync(request);

            if (account == null)
            {
                response = HiTechApi.ResponseBadRequest();
                return BadRequest(response);
            }

            response = HiTechApi.Response(201, "Created.", account);
            return CreatedAtAction("GetAccount",
                new { id = ((ApiResponse<AccountResponse>)response)?.Data?.AccountId }, response);
        }

        // DELETE: api/hitech/account/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteAccount(int id)
        {
            var response = HiTechApi.ResponseNotFound();

            if (!await _accountService.AccountExists(id))
            {
                return NotFound(response);
            }

            bool flag = await _accountService.DeleteAsync(id);
            if (flag)
            {
                return NoContent();
            }
            response = HiTechApi.ResponseBadRequest();
            return BadRequest(response);
        }
    }
}
