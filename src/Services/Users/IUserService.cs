using System.Threading;
using System.Threading.Tasks;
using Models.Users;

namespace Services.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Creates a user. Usernames are unique. Throws a QuoterException if the username has been taken
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> CreateUser(CreateUserDTO dto, CancellationToken token = default);

        /// <summary>
        /// Get's a user by id. If the user already exists, just return that users Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<GetUserDTO> GetUserById(int id, CancellationToken token = default);

        /// <summary>
        /// Updates a user. Throws a QuoterException if the user is not found
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> UpdateUser(UpdateUserDTO dto, CancellationToken token = default);
    }
}