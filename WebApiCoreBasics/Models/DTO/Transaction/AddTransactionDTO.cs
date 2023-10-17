using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiCoreBasics.Models.DTO.Transaction
{
    public class AddTransactionDTO
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public double Amount { get; set; }
        public string Details { get; set; }

    }
}
