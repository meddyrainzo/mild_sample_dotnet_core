using System.Threading;
using System.Threading.Tasks;
using Models.Exceptions;
using Models.Users;
using Repository.Users;

namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateUser(CreateUserDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            var request = CreateUserRequest.CreateRequest(dto.UserName);
            var user = await _repository.GetUserByUsername(request.UserName).ConfigureAwait(false);
            // If user already exists, just return that users id
            if (user != null)
                return user.Id;
            return await _repository.CreateUser(request, token).ConfigureAwait(false);
        }

        public async Task<GetUserDTO> GetUserById(int id, CancellationToken token = default)
        {
            id.CheckInvalid();
            var user = await _repository.GetUserById(id, token).ConfigureAwait(false);
            return user ?? throw new QuoterException(ErrorReason.USER_NOT_FOUND);
        }

        public async Task<int> UpdateUser(UpdateUserDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            var user = await GetUserById(dto.Id, token).ConfigureAwait(false);
            var request = UpdateUserRequest.CreateRequest(user.Id, dto.UserName);
            return await _repository.UpdateUser(request, token).ConfigureAwait(false);
        }
    }
}