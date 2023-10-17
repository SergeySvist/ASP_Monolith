namespace WebApiCoreBasics.Models.DTO.User
{
    public class EditUserPasswordDTO
    {
        public long Id { get; set; }
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

    }
}
