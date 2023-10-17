using WebApiCoreBasics.DataLayer;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataLayer _dataLayer;

        public AccountService (IAccountDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }
        public async Task<Account> CreateAccountForUser(User addedUser)
        {
            if(addedUser != null && addedUser.Id > 0)
                return await _dataLayer.CreateForUser(addedUser);
            
            return null;
        }

        public async Task<Account> GetByUser(User user)
        {
            return await _dataLayer.GetByUser(user);
        }
    }
}
