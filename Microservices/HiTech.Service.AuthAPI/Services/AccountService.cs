using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Service.AuthAPI.UOW;
using HiTech.Service.AuthAPI.Utils;

namespace HiTech.Service.AuthAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AccountExists(int id)
        {
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);
            return account != null;
        }

        public async Task<bool> AccountExists(string email)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(email);
            return account != null;
        }

        public async Task<AccountResponse?> CreateAsync(AccountCreationRequest request)
        {
            var account = _mapper.Map<Account>(request);
            account.Password = PasswordEncoder.Encode(request.Password);

            Account? response = null;
            try
            {
                response = await _unitOfWork.Accounts.CreateAsync(account);
                await _unitOfWork.SaveAsync();
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
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);

            if (account != null)
            {
                try
                {
                    _unitOfWork.Accounts.Delete(account);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the account at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            return result;
        }

        public async Task<IEnumerable<AccountResponse>> GetAllAsync()
        {
            var accounts = await _unitOfWork.Accounts.GetAllAsync();
            return _mapper.Map<IEnumerable<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse?> GetByIDAsync(int id)
        {
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<AccountResponse?> GetByEmailAsync(string email)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(email);

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task<bool> UpdateAsync(int id, AccountUpdationRequest request)
        {
            bool result = false;
            var account = await _unitOfWork.Accounts.GetByIDAsync(id);

            if (account != null)
            {
                try
                {
                    _mapper.Map(request, account);

                    _unitOfWork.Accounts.Update(account);
                    result = await _unitOfWork.SaveAsync() > 0;
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
