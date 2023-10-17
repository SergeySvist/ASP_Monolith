using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiCoreBasics.Models.DbCtx;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.DataLayer
{
    public class TransactionDataLayer : ITransactionDataLayer
    {
        public async Task<Transaction> GetById(long id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Transaction transactionById = await db.Transactions.FirstOrDefaultAsync(x => x.Id == id);

                return transactionById != null ? transactionById : new Transaction();
            }
        }

        public async Task<List<Transaction>> GetByUserId(long id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Expression<Func<Transaction, bool>> userTransactionSearchPredicate = (t) => t.UserId == id; 

                if(db.Transactions.Any(userTransactionSearchPredicate))
                    return await db.Transactions.Where(userTransactionSearchPredicate).ToListAsync();

                return new List<Transaction>();
            }
        }
    }
}
