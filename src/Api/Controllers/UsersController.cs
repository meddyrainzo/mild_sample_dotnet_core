using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Exceptions;
using Models.Users;
using Services.Users;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _service.GetUserById(id, default).ConfigureAwait(false);
                return Ok(user);
            }
            catch (QuoterException ex) when (ex.Message == ErrorReason.USER_NOT_FOUND)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
        {
            try
            {
                var createdUserId = await _service.CreateUser(dto, default).ConfigureAwait(false);
                return Ok(new { Id = createdUserId });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            try
            {
                var updated = await _service.UpdateUser(dto).ConfigureAwait(false);
                if (updated == 1)
                    return Ok(new { Id = dto.Id });
                return BadRequest(new { ErrorMessage = "Failed to update user" });
            }
            catch (QuoterException ex) when (ex.Message == ErrorReason.USER_NOT_FOUND)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}