using Mapster;
using WebApiCoreBasics.Models.DTO.Transaction;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionIndexser _transactionIndexser;
        private readonly ITransactionDataLayer _transactionDataLayer;
        public TransactionService(ITransactionIndexser transactionIndexser, ITransactionDataLayer transactionDataLayer)
        {
            _transactionIndexser = transactionIndexser;
            _transactionDataLayer = transactionDataLayer;
        }

        public async Task<TransactionDTO> Add(AddTransactionDTO transactionDTO)
        {
            Transaction transactionToAddFromDTO = transactionDTO.Adapt<Transaction>();

            Transaction appliedTransaction = await _transactionIndexser.ApplyTransaction(transactionToAddFromDTO);

            return appliedTransaction.Adapt<TransactionDTO>();
        }

        public async Task<TransactionDTO> GetById(long id)
        {
            Transaction transactionById = await _transactionDataLayer.GetById(id);

            return transactionById.Adapt<TransactionDTO>();
        }

        public async Task<List<TransactionDTO>> GetByUserId(long id)
        {
            List<Transaction> transactions = await _transactionDataLayer.GetByUserId(id);

            return transactions.Adapt<List<TransactionDTO>>();
        }
    }
}
