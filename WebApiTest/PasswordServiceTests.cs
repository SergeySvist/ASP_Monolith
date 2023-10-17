using WebApiCoreBasics.Services;

namespace WebApiTest
{
    public class PasswordServiceTests
    {
        private PasswordService passwordService = new PasswordService();
        private string rightPassword = "rightPassword";
        private string wrongPassword = "wrongPassword";
        private const int HASH_SIZE = 32;
        [Fact]
        public void ShouldCreateHashOnInputString()
        {
            byte[] hashOfString = passwordService.CreatePasswordHash(rightPassword);

            Assert.NotEmpty(hashOfString);
            Assert.NotNull(hashOfString);
            Assert.True(hashOfString.Length >= HASH_SIZE);
        }

        [Fact]
        public void ShouldReturnFalseOnHashCheckWithWrongLength()
        {
            bool hashValidationResult = passwordService.ValidatePasswordAgainstHash(new byte[HASH_SIZE+1], wrongPassword);

            Assert.False(hashValidationResult);
        }

        [Fact]
        public void ShouldReturnTrueOnHashCheckAgainstRigthHash()
        {
            byte[] hashOfRightPassword = passwordService.CreatePasswordHash(rightPassword);

            bool hashValidationResult = passwordService.ValidatePasswordAgainstHash(hashOfRightPassword, rightPassword);

            Assert.True(hashValidationResult);
        }

        [Fact]
        public void ShouldReturnFalseOnHashCheckWithWrongPassword()
        {
            byte[] hashOfRightPassword = passwordService.CreatePasswordHash(rightPassword);

            bool hashValidationResult = passwordService.ValidatePasswordAgainstHash(hashOfRightPassword, wrongPassword);

            Assert.False(hashValidationResult);
        }
    }
}