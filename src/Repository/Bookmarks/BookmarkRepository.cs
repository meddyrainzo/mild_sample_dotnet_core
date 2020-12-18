using Dapper;
using Models.Bookmarks;
using Models.Exceptions;
using Models.Quotes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Bookmarks
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public BookmarkRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateBookmark(CreateBookmarkRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QuerySingleOrDefaultAsync<int>(BookmarkSqlStatements.createdBookmark, request).ConfigureAwait(false);
        }

        public async Task<GetBookmarkDTO> GetBookmarkById(int id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QuerySingleOrDefaultAsync<GetBookmarkDTO>(BookmarkSqlStatements.getBookmarkById, new { id = id });
        }

        public async Task<IEnumerable<GetBookmarkDTO>> GetBookmarks(int userid, int skip, int limit, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryAsync<GetBookmarkDTO, GetQuoteDetailsDTO, GetBookmarkDTO>(BookmarkSqlStatements.getBookmarks,
            (bookmark, quote) =>
            {
                bookmark.Quote = quote;
                return bookmark;
            }
            , new { userid = userid, skip = skip, limit = limit });
        }

        public async Task<int> RemoveBookmark(int id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(BookmarkSqlStatements.removeBookmark, new { id = id });
        }
    }
}