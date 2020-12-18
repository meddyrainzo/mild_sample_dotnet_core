using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Quotes;

namespace Services.Quotes
{
    public interface IQuoteService
    {
        /// <summary>
        /// Get a paged list of quotes. Defaults to 10 quotes returned (If there is up to 10 quotes)
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="userId"></param>
        /// <param name="limit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<GetQuoteDetailsDTO>> GetQuotes(int userId, int skip = 0, int limit = 10, CancellationToken token = default);

        /// <summary>
        /// Gets a single quote and throws a QuoterException if quote not found
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<GetQuoteDetailsDTO> GetQuote(int id, int userId, CancellationToken token = default);

        /// <summary>
        /// Create a quote
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> CreateQuote(CreateQuoteDTO dto, CancellationToken token = default);
        /// <summary>
        /// Update a quote. Throws a QuoterException if the quote does not exist
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> UpdateQuote(UpdateQuoteDTO dto, CancellationToken token = default);

        /// <summary>
        /// Deletes a quote. Throws a QuoterException if the quote does not exist
        /// </summary>
        /// <param name="quoteId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> DeleteQuote(int quoteId, CancellationToken token = default);
    }
}