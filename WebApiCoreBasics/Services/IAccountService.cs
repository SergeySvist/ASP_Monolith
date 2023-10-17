using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public interface IAccountService
    {
        Task<Account> CreateAccountForUser(User addedUser);
        Task<Account> GetByUser(User user);
    }
}