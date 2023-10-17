using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics
{
    public interface ITransactionDataLayer
    {
        Task<Transaction> GetById(long id);
        Task<List<Transaction>> GetByUserId(long id);
    }
}