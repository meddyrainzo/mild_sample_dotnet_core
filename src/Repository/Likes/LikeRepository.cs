using System.Threading;
using System.Threading.Tasks;
using Models.Exceptions;
using Models.Likes;
using Dapper;

namespace Repository.Likes
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public LikeRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<GetLikeDTO> GetLike(GetLikeRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<GetLikeDTO>(LikeSqlStatements.getLike, request).ConfigureAwait(false);
        }

        public async Task<int> Like(CreateLikeRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(LikeSqlStatements.like, request).ConfigureAwait(false);
        }

        public async Task<int> Unlike(CreateLikeRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(LikeSqlStatements.unlike, request).ConfigureAwait(false);
        }

        public Task<int> Unlike(UnlikeRequest request, CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}