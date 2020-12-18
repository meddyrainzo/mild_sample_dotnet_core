using System.Threading;
using System.Threading.Tasks;
using Models.Users;
using Dapper;

namespace Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        public UserRepository(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateUser(CreateUserRequest request, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            var id = await connection.QuerySingleOrDefaultAsync<int>(UserSqlStatements.createUser, request).ConfigureAwait(false);
            return id;
        }

        public async Task<GetUserDTO> GetUserById(int userid, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<GetUserDTO>(UserSqlStatements.getUserById, new { id = userid }).ConfigureAwait(false);
        }

        public async Task<GetUserDTO> GetUserByUsername(string username, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.QueryFirstOrDefaultAsync<GetUserDTO>(UserSqlStatements.getuserByName, new { username = username }).ConfigureAwait(false);
        }

        public async Task<int> UpdateUser(UpdateUserRequest updatedUser, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetOpenConnection();
            return await connection.ExecuteAsync(UserSqlStatements.updateUser, updatedUser).ConfigureAwait(false);
        }
    }
}