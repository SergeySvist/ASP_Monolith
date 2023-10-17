using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.DataLayer
{
    public interface IAccountDataLayer
    {
        Task<Account> CreateForUser(User addedUser);
        Task<Account> GetByUser(User user);
    }
}
