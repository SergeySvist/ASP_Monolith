using Microsoft.AspNetCore.Mvc;
using WebApiCoreBasics.Models.DTO.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiCoreBasics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> Get()
        {
            return await _userService.GetAllUsers(); 
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTO> Get(long id)
        {
            return await _userService.GetUserByID(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserDTO> Post([FromBody] AddUserDTO userToAdd)
        {
            return await _userService.CreateUserFromDTO(userToAdd);
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        public async Task<bool> Put([FromBody] EditUserDTO userToEdit)
        {
            return await _userService.UpdateUserFromDTO(userToEdit);
        }

        [HttpPut("updateuserpassword")]
        public async Task<bool> Put([FromBody] EditUserPasswordDTO userToEditPassword)
        {
            return await _userService.UpdateUserPasswordFromDTO(userToEditPassword);

        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(long id)
        {
            return await _userService.DeleteUserByID(id);
        }
    }
}
