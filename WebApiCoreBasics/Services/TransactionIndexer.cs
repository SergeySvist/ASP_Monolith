using Microsoft.EntityFrameworkCore;
using WebApiCoreBasics.DataLayer;
using WebApiCoreBasics.Models.DbCtx;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public class TransactionIndexer : ITransactionIndexser
    {        
        private readonly Transaction failedTransaction = new Transaction { Id = -1 };
        private readonly IAccountDataLayer _accountDataLayer;

        public TransactionIndexer(IAccountDataLayer accountDataLayer)
        {
            _accountDataLayer = accountDataLayer;
        }

        public async Task<Transaction> ApplyTransaction(Transaction transactionToAdd)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User userForTransaction = await db.Users.FirstOrDefaultAsync(u => u.Id == transactionToAdd.UserId);

                if (userForTransaction == null) return failedTransaction;

                Account accountForTransaction = await db.Accounts.FirstOrDefaultAsync(u => u.Id == transactionToAdd.UserId);

                if (accountForTransaction == null)
                {
                    Account createdAccount = await _accountDataLayer.CreateForUser(userForTransaction);
                    accountForTransaction = await db.Accounts.FirstOrDefaultAsync(a => a.Id == createdAccount.Id);
                }

                if(accountForTransaction.Id == 0) return failedTransaction;

                using (var transaction = db.Database.BeginTransaction())
                {
                    accountForTransaction.Balace += transactionToAdd.Amount;

                    int accountRowsUpdated = await db.SaveChangesAsync();

                    if (accountRowsUpdated == 0)
                    {
                        transaction.Rollback();
                        return failedTransaction;
                    }

                    transactionToAdd.User = userForTransaction;
                    db.Transactions.Add(transactionToAdd);

                    int transactionsAdded = await db.SaveChangesAsync();

                    if (transactionsAdded == 0)
                    {
                        transaction.Rollback();
                        return failedTransaction;
                    }

                    transaction.Commit();
                    return transactionToAdd;
                }
            }
        }
    }
}
