using Microsoft.EntityFrameworkCore;
using WebApiCoreBasics.Models.DbCtx;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.DataLayer
{
    public class AccountDataLayer : IAccountDataLayer
    {
        public async Task<Account> CreateForUser(User addedUser)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User? accountUser = db.Users.FirstOrDefault(u => u.Id == addedUser.Id);

                if(accountUser != null)
                {
                    Account accountForUser = new Account();
                    accountForUser.User = accountUser;

                    db.Accounts.Add(accountForUser);

                    await db.SaveChangesAsync();

                    return accountForUser;
                }

                return new Account();
            }
        }

        public async Task<Account> GetByUser(User user)
        {
            using (MyDbContext db = new MyDbContext())
            {
                return await db.Accounts.FirstOrDefaultAsync(a => a.User.Id == user.Id);
            }
        }
    }
}
