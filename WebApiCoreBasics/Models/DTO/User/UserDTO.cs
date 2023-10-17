namespace WebApiCoreBasics.Models.DTO.User
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisteredAt { get; set; }
        public double Balance { get; set; }
    }
}
