using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Bookmarks;
using Models.Exceptions;
using Services.Bookmarks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _service;

        public BookmarksController(IBookmarkService service)
        {
            _service = service;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBookmarks(int userId, int skip = 0, int limit = 10)
        {
            try
            {
                var comments = await _service.GetBookmarks(userId, skip, limit).ConfigureAwait(false);
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
        public async Task<IActionResult> CreateBookmark([FromBody] CreateBookmarkDTO dto)
        {
            try
            {
                var commentId = await _service.CreateBookmark(dto).ConfigureAwait(false);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBookmark(int id)
        {
            try
            {
                var commentId = await _service.RemoveBookmark(id).ConfigureAwait(false);
                return NoContent();
            }
            catch (QuoterException ex) when (ex.Message == ErrorReason.BOOKMARK_NOT_FOUND)
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