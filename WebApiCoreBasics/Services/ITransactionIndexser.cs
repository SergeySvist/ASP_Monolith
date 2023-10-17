using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public interface ITransactionIndexser
    {
        Task<Transaction> ApplyTransaction(Transaction transactions);
    }
}
