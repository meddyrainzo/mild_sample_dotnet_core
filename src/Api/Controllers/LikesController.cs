using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Exceptions;
using Models.Likes;
using Services.Likes;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _service;

        public LikesController(ILikeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> LikeOrUnlike([FromBody] CreateLikeDTO dto)
        {
            try
            {
                var successful = await _service.LikeOrUnlike(dto).ConfigureAwait(false);
                if (successful == 1)
                    return NoContent();
                return BadRequest(new { Message = "There was an error liking/unliking the quote" });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}