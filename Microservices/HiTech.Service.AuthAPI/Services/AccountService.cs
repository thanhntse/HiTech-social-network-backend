using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Repositories;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Service.AuthAPI.Utils;

namespace HiTech.Service.AuthAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AccountExists(int id)
        {
            var account = await _accountRepository.GetByIDAsync(id);
            return account != null;
        }

        public async Task<bool> AccountExists(string email)
        {
            var account = await _accountRepository.GetByEmailAsync(email);
            return account != null;
        }

        public async Task<AccountResponse?> CreateAsync(AccountCreationRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.Password = PasswordEncoder.Encode(request.Password);

            Account? response = null;
            try
            {
                response = await _accountRepository.CreateAsync(account);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the account at {Time}.", DateTime.Now);
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
                try
                {
                    result = await _accountRepository.DeleteAsync(account);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the account at {Time}.", DateTime.Now);
                    result = false;
                }
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

        public async Task<AccountResponse?> GetByEmailAsync(string email)
        {
            var account = await _accountRepository.GetByEmailAsync(email);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<bool> UpdateAsync(int id, AccountUpdationRequest request)
        {
            bool result = false;
            var account = await _accountRepository.GetByIDAsync(id);

            if (account != null)
            {
                try
                {
                    _mapper.Map(request, account);

                    result = await _accountRepository.UpdateAsync(account);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the account at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            return result;
        }
    }
}
