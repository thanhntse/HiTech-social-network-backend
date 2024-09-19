using AutoMapper;
using HiTech.Service.AccountAPI.DTOs.Request;
using HiTech.Service.AccountAPI.DTOs.Response;
using HiTech.Service.AccountAPI.Entities;
using HiTech.Service.AccountAPI.Repositories;
using HiTech.Service.AccountAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AccountAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<bool> AccountExists(int id)
        {
            return await _accountRepository.GetAll().AsQueryable().AnyAsync(a => a.AccountId == id);
        }

        public async Task<bool> AccountExists(string email)
        {
            return await _accountRepository.GetAll().AsQueryable().AnyAsync(a => a.Email == email);
        }

        public async Task<AccountResponse?> CreateAsync(AccountRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.Password = PasswordEncoder.Encode(request.Password);

            Account? response = null;
            try
            {
                response = await _accountRepository.CreateAsync(account);
            }
            catch (Exception)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var account = await _accountRepository.GetByIDAsync(id);

            if (account != null)
            {
                result = await _accountRepository.DeleteAsync(account);
            }

            return result;
        }

        public async IAsyncEnumerable<AccountResponse> GetAllAsync()
        {
            await foreach (var account in _accountRepository.GetAllAsync())
            {
                yield return _mapper.Map<AccountResponse>(account);
            }
        }

        public async Task<AccountResponse?> GetByIDAsync(int id)
        {
            var account = await _accountRepository.GetByIDAsync(id);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<bool> UpdateAsync(int id, AccountRequest request)
        {
            bool result = false;
            var account = await _accountRepository.GetByIDAsync(id);

            if (account != null)
            {
                _mapper.Map(request, account);
                account.Password = PasswordEncoder.Encode(request.Password);

                result = await _accountRepository.UpdateAsync(account);
            }

            return result;
        }
    }
}
