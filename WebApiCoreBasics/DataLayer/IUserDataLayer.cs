using WebApiCoreBasics.Models.DTO.User;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics
{
    public interface IUserDataLayer
    {
        Task<User> AddUser(User newuser);
        Task<bool> DeleteUserById(long id);
        Task<List<User>> GetAll();
        Task<User> GetUserById(long id);
        Task<bool> UpdateUser(User userToEdit);
        Task<bool> UpdateUserPassword(long id, byte[] passwordHash);
    }
}