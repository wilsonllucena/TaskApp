using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApp.Interfaces;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    [Authorize] // Bloqueia para acessar com token 
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
     
        public UserController(IUserRepository repository)
        {
            _userRepository = repository;
        }  
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            var users = await _userRepository.getUsers();

            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            var user = await _userRepository.getUserById(id);

            return Ok(user);
        }
        
        [HttpPost()]
        public async Task<ActionResult<UserModel>> AddUser([FromBody] UserModel user)
        {
            var userModel = await _userRepository.addUser(user);
            return Ok(userModel);
        }
        
        [HttpPatch("{id}")]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody] UserModel user, int id)
        {
            var userModel = await _userRepository.updateUser(user, id);
            return Ok(userModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> RemoveUser(int id)
        {
            var user = await _userRepository.removeUser(id);

            return Ok(user);
        }
    }
}
