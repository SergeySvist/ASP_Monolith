namespace WebApiCoreBasics.Models.DTO.Transaction
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public double Amount { get; set; }
        public string Details { get; set; }
        public DateTime DateTime { get; set; }

    }
}
