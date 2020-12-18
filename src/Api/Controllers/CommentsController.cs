using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Comments;
using Models.Exceptions;
using Services.Comments;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet("{quoteId}")]
        public async Task<IActionResult> GetComments(int quoteId)
        {
            try
            {
                var comments = await _service.GetComments(quoteId).ConfigureAwait(false);
                return Ok(comments);
            }
            catch (QuoterException ex) when (ex.Message == ErrorReason.QUOTE_NOT_FOUND)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO dto)
        {
            try
            {
                var commentId = await _service.CreateComment(dto).ConfigureAwait(false);
                return Ok(new { Id = commentId });
            }
            catch (QuoterException ex) when (ex.Message == ErrorReason.QUOTE_NOT_FOUND)
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