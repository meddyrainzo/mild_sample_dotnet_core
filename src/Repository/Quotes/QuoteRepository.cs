using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Exceptions;
using Models.Quotes;
using Dapper;

namespace Repository.Quotes
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public QuoteRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> CreateQuote(CreateQuoteRequest request, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.QuerySingleOrDefaultAsync<int>(QuoteSqlStatements.createQuote, request).ConfigureAwait(false);
        }

        public async Task<int> DeleteQuote(int quoteId, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(QuoteSqlStatements.deleteQuote, new { id = quoteId });
        }

        public async Task<IEnumerable<GetQuoteDetailsDTO>> GetQuotes(int userId, int skip = 0, int limit = 10, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryAsync<GetQuoteDetailsDTO>(QuoteSqlStatements.getQuotes, new { userId = userId, skip = skip, limit = limit }).ConfigureAwait(false);
        }

        public async Task<GetQuoteDTO> GetQuote(int id, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<GetQuoteDTO>(QuoteSqlStatements.getSingleQuoteById, new { id = id }).ConfigureAwait(false);
        }

        public async Task<int> UpdateQuote(UpdateQuoteRequest request, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(QuoteSqlStatements.updateQuote, request);
        }

        public async Task<GetQuoteDetailsDTO> GetQuoteDetails(int userId, int id, CancellationToken token = default)
        {
            var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<GetQuoteDetailsDTO>(QuoteSqlStatements.getSingleQuoteDetails, new { userId = userId, id = id }).ConfigureAwait(false);
        }
    }
}