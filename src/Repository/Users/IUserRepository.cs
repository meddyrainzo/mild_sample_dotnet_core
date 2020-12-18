using System.Threading;
using System.Threading.Tasks;
using Models.Users;

namespace Repository.Users
{
    public interface IUserRepository
    {
        Task<int> CreateUser(CreateUserRequest request, CancellationToken token = default);
        Task<GetUserDTO> GetUserById(int id, CancellationToken token = default);
        Task<GetUserDTO> GetUserByUsername(string username, CancellationToken token = default);
        Task<int> UpdateUser(UpdateUserRequest request, CancellationToken token = default);
    }
}