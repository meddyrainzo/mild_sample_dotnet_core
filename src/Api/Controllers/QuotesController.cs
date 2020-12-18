using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.Exceptions;
using Models.Quotes;
using Services.Quotes;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuoteService _service;

        public QuotesController(IQuoteService service)
        {
            _service = service;
        }

        [HttpGet("{currentUserId}")]
        public async Task<IEnumerable<GetQuoteDetailsDTO>> GetQuotes(int currentUserId, int from = 0, int limit = 10)
        {
            return await _service.GetQuotes(currentUserId, from, limit).ConfigureAwait(false);
        }

        [HttpGet("{id}/{currentUserId}")]
        public async Task<IActionResult> GetQuote(int id, int currentUserId)
        {
            try
            {
                var quote = await _service.GetQuote(id, currentUserId).ConfigureAwait(false);
                return Ok(quote);
            }
            catch (QuoterException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteDTO dto)
        {
            try
            {
                var id = await _service.CreateQuote(dto).ConfigureAwait(false);
                return Ok(new { Id = id });
            }
            catch (QuoterException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuote([FromBody] UpdateQuoteDTO dto)
        {
            try
            {
                var updated = await _service.UpdateQuote(dto).ConfigureAwait(false);
                if (updated == 1)
                    return Ok(new { Id = dto.Id });
                return BadRequest(new { Message = "There was a problem updating the quote" });
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
        public async Task<IActionResult> DeleteQuote(int id)
        {
            try
            {
                var deleted = await _service.DeleteQuote(id).ConfigureAwait(false);
                if (deleted == 1)
                    return NoContent();
                return BadRequest(new { Message = "There was an error deleting the quote" });
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