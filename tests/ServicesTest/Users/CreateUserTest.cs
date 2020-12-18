using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Users;
using Repository.Users;
using Services.Users;
using Models.Exceptions;

namespace ServicesTest.Users
{
    public class CreateUserTest
    {
        private readonly CreateUserDTO _dto;
        private readonly Mock<IUserRepository> _mockRepository;
        public CreateUserTest()
        {
            _dto = new CreateUserDTO { UserName = "username" };
            _mockRepository = new Mock<IUserRepository>();
            _mockRepository.Setup(r => r.CreateUser(It.IsAny<CreateUserRequest>(), default)).ReturnsAsync(1);
        }

        [Fact]
        public async Task CreateUserSuccessfully()
        {
            //Given
            _mockRepository.Setup(r => r.GetUserByUsername(It.IsAny<string>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new UserService(_mockRepository.Object);

            //When
            var userId = await service.CreateUser(_dto, default);

            //Then
            Assert.Equal(1, userId);
        }

        [Fact]
        public async Task CreateUserThatAlreadyExistsShouldReturnThatUserr()
        {
            //Given
            var id = 1;
            _mockRepository.Setup(r => r.GetUserByUsername(It.IsAny<string>(), default)).ReturnsAsync(new GetUserDTO { Id = id });
            var service = new UserService(_mockRepository.Object);

            //When
            var userId = await service.CreateUser(_dto, default);

            //Then
            Assert.Equal(id, userId);
        }

        [Fact]
        public void CreateUserNullDTOShouldFail()
        {
            //Given
            var service = new UserService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.CreateUser(null, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }

    }
}