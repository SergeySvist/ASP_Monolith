using Microsoft.AspNetCore.Mvc;
using WebApiCoreBasics.Models.DTO.Transaction;
using WebApiCoreBasics.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoreBasics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET api/<TransactionController>/5
        //By Transaction Id
        [HttpGet("{id}")]
        public async Task<TransactionDTO> Get(long id)
        {
            return await _transactionService.GetById(id);
        }
        
        // GET api/<TransactionController>/user/5
        //By User Id
        [HttpGet("user/{id}")]
        public async Task<List<TransactionDTO>> GetByUserId(long id)
        {
            return await _transactionService.GetByUserId(id);
        }

        // POST api/<TransactionController>
        [HttpPost]
        public async Task<TransactionDTO> Post([FromBody] AddTransactionDTO newTransaction)
        {
            //ToDo: Use modelState to validate model
            return await _transactionService.Add(newTransaction);

        }

    }
}
