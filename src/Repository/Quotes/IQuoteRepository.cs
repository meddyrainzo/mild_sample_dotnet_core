using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Quotes;

namespace Repository.Quotes
{
    public interface IQuoteRepository
    {
        Task<IEnumerable<GetQuoteDetailsDTO>> GetQuotes(int userId, int from = 0, int limit = 10, CancellationToken token = default);
        Task<GetQuoteDetailsDTO> GetQuoteDetails(int userId, int id, CancellationToken token = default);
        Task<GetQuoteDTO> GetQuote(int id, CancellationToken token = default);
        Task<int> CreateQuote(CreateQuoteRequest request, CancellationToken token = default);
        Task<int> UpdateQuote(UpdateQuoteRequest request, CancellationToken token = default);
        Task<int> DeleteQuote(int quoteId, CancellationToken token = default);
    }
}