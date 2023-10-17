using WebApiCoreBasics.Models.DTO.User;

namespace WebApiCoreBasics
{
    public interface IUserService
    {
        Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd);
        Task<bool> DeleteUserByID(long id);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserByID(long id);
        Task<bool> UpdateUserFromDTO(EditUserDTO userToEdit);
        Task<bool> UpdateUserPasswordFromDTO(EditUserPasswordDTO userToEditPassword);
    }
}