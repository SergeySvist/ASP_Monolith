using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WebApiCoreBasics;
using WebApiCoreBasics.DataLayer;
using WebApiCoreBasics.Models.DTO.User;
using WebApiCoreBasics.Models.Entities;
using WebApiCoreBasics.Services;

namespace WebApiTest
{
    public class UserServiceTests
    {
        private static IPasswordService passwordService = new PasswordService();
        private static IAccountService accountService = new AccountService(new AccountDataLayer());

        private static IUserDataLayer dataLayer = new Mock<IUserDataLayer>().Object;
        private static string userValidPassword = "MyStrongPassword";
        private static string userNewPassword = "123456";
        private static long userValidID = 1;
        private static long userInvalidID = 0;

        private readonly IUserService userService;
        private AddUserDTO userToAdd = new AddUserDTO() { Name = "New User", Password = "password"};
        private User validUserFromDB = new User() { Id=userValidID, Name="New User", RegisteredAt = DateTime.Now, PasswordHash = passwordService.CreatePasswordHash(userValidPassword) };

        public UserServiceTests()
        {
            Mock<IUserDataLayer> dataLayerMock = new Mock<IUserDataLayer>();

            dataLayerMock.Setup(m => m.AddUser(It.IsAny<User>()).Result).Returns(validUserFromDB);
            dataLayerMock.Setup(m => m.GetUserById(It.Is<long>(id => id == 0)).Result).Returns(new User());
            dataLayerMock.Setup(m => m.GetUserById(It.Is<long>(id => id > 0)).Result).Returns(validUserFromDB);
            dataLayerMock.Setup(m => m.UpdateUser(It.IsAny<User>()).Result).Returns(true);
            dataLayerMock.Setup(m => m.UpdateUserPassword(It.IsAny<long>(), It.IsAny<byte[]>()).Result).Returns(true);

            dataLayer = dataLayerMock.Object;

            userService = new UserService(passwordService, dataLayer, accountService);
        }

        [Fact]
        public async void ShouldCreateUserFromDto()
        {
            UserDTO addedUser = await userService.CreateUserFromDTO(userToAdd);

            Assert.NotNull(addedUser);

        }

        [Fact]
        public async void ReturnsFalseOnTryingToUpdateNonExistenUser()
        {
            EditUserDTO editUserDTO = new EditUserDTO { Id = userInvalidID, Name = "User name updated" };

            bool isUserUpdated = await userService.UpdateUserFromDTO(editUserDTO);

            Assert.False(isUserUpdated);
        }

        [Fact]
        public async void ReturnsFalseOnTryingToUpdateExistenUser()
        {
            EditUserDTO editUserDTO = new EditUserDTO { Id = userValidID, Name = "User name updated" };

            bool isUserUpdated = await userService.UpdateUserFromDTO(editUserDTO);

            Assert.True(isUserUpdated);
        }

        [Fact]
        public async void ShouldNotUpdatePasswordForNonExistebtUser()
        {
            EditUserPasswordDTO editUserPasswordDTO = new EditUserPasswordDTO { Id = userInvalidID, OldPassword = userValidPassword, NewPassword = userNewPassword };

            bool isPasswordUpdated = await userService.UpdateUserPasswordFromDTO(editUserPasswordDTO);

            Assert.False(isPasswordUpdated);
        }

        [Fact]
        public async void ShouldReturnTrueOnTryingUpdateKnownPasswordForExistenUser()
        {
            EditUserPasswordDTO editUserPasswordDTO = new EditUserPasswordDTO { Id = userValidID, OldPassword = userValidPassword, NewPassword = userNewPassword };
            
            bool isPasswordUpdated = await userService.UpdateUserPasswordFromDTO(editUserPasswordDTO);

            Assert.True(isPasswordUpdated);

        }

        [Fact]
        public async void ShouldReturnFalseOnTryingUpdateUnknownPasswordForExistenUser()
        {
            EditUserPasswordDTO editUserPasswordDTO = new EditUserPasswordDTO { Id = userValidID, OldPassword = "SomeInvalidOldPass", NewPassword = userNewPassword };

            bool isPasswordUpdated = await userService.UpdateUserPasswordFromDTO(editUserPasswordDTO);

            Assert.False(isPasswordUpdated);

        }

    }
}
