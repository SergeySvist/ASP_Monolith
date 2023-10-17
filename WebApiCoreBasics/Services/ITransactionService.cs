using WebApiCoreBasics.Models.DTO.Transaction;

namespace WebApiCoreBasics.Services
{
    public interface ITransactionService
    {
        
        Task<TransactionDTO> Add(AddTransactionDTO transactionDTO);
        Task<TransactionDTO> GetById(long id);
        Task<List<TransactionDTO>> GetByUserId(long id);
    }
}
