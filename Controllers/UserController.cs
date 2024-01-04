using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TODOApi.Interfaces;
using TODOApi.Models;
using TODOApi.Services;
using TODOApi.Interfaces;
using TODOApi.Models;

namespace TODOApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordRepository _userPasswordRepository;

        public UserController(
        ILogger<UserController> logger,
        IUserRepository userRepository,
        IUserPasswordRepository userPasswordRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userPasswordRepository = userPasswordRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var entity = await _userRepository.GetByIdAsync(id);

                if (entity == null)
                {
                    return NotFound();
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("login")]
        public async Task<ActionResult<User>> GetLogin(string login, string password)
        {
            try
            {
                var user = await _userRepository.GetAsync(login);
            
                if (user == null) { return NotFound(); }

                var passwordVerify = BCrypt.Net.BCrypt.Verify(password, user.Password.Password);

                if (passwordVerify == false)
                {
                    return BadRequest();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");

                return Problem(ex.Message);
            }

        }

        [HttpPost]
        public async Task Register(string login, string password)
        {
            try
            {
                await _userRepository.PostAsync(login, password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }

        [HttpPut]
        public async Task Update(string login, string password)
        {
            try
            {
                await _userRepository.PutAsync(login, password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }
        }
    }
}
