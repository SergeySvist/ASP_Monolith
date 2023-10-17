using Mapster;
using WebApiCoreBasics.Models.DTO.User;
using WebApiCoreBasics.Models.Entities;

namespace WebApiCoreBasics.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserDataLayer _userDataLayer;
        private readonly IAccountService _accountService;

        public UserService(IPasswordService passwordService, IUserDataLayer userDataLayer, IAccountService accountService)
        {
            _passwordService = passwordService;
            _userDataLayer = userDataLayer;
            _accountService = accountService;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            List<User> allUsers = await _userDataLayer.GetAll();

            return allUsers.Adapt<List<UserDTO>>();

        }
        public async Task<UserDTO> GetUserByID(long id)
        {
            User userById = await _userDataLayer.GetUserById(id);

            Account userAccount = await _accountService.GetByUser(userById);

            UserDTO userDTO = userById.Adapt<UserDTO>();

            if (userAccount != null) userDTO.Balance = userAccount.Balace;

            return userDTO;
        }
        public async Task<UserDTO> CreateUserFromDTO(AddUserDTO userToAdd)
        {
            User newuser = userToAdd.Adapt<User>();
            newuser.PasswordHash = _passwordService.CreatePasswordHash(userToAdd.Password);

            User addedUser = await _userDataLayer.AddUser(newuser);

            Account UserAccount = await _accountService.CreateAccountForUser(addedUser);

            return addedUser.Adapt<UserDTO>();
        }
        public async Task<bool> UpdateUserFromDTO(EditUserDTO userToEdit)
        {
            User userToUpdate = await _userDataLayer.GetUserById(userToEdit.Id);
            if (userToUpdate.Id == 0)
                return false;

            userToUpdate.Name = userToEdit.Name;
            return await _userDataLayer.UpdateUser(userToUpdate);
        }
        public async Task<bool> DeleteUserByID(long id)
        {
            return await _userDataLayer.DeleteUserById(id);
        }

        public async Task<bool> UpdateUserPasswordFromDTO(EditUserPasswordDTO userToEditPassword)
        {
            User userById = await _userDataLayer.GetUserById(userToEditPassword.Id);

            if (userById.Id == 0) return false;

            Byte[] oldPasswordHash = userById.PasswordHash;
            if(_passwordService.ValidatePasswordAgainstHash(oldPasswordHash, userToEditPassword.OldPassword))
            {
                userById.PasswordHash = _passwordService.CreatePasswordHash(userToEditPassword.NewPassword);

                bool isPasswordUpdated = await _userDataLayer.UpdateUserPassword(userById.Id, userById.PasswordHash);
                return true;
            }
            return false;
        }
    }
}
