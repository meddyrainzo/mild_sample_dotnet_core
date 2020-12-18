using Dapper;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Comments;
using Models.Exceptions;

namespace Repository.Comments
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public CommentRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateComment(CreateCommentRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(CommentSqlStatements.createComment, request).ConfigureAwait(false);
        }

        public async Task<IEnumerable<GetCommentDTO>> GetComments(int quoteId, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryAsync<GetCommentDTO>(CommentSqlStatements.getComments, new { quoteid = quoteId });
        }
    }
}