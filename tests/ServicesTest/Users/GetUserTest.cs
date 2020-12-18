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
    public class GetUserTest
    {
        private readonly Mock<IUserRepository> _mockRepository;
        public GetUserTest()
        {
            _mockRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetUserByIdSuccessfully()
        {
            //Given
            var username = "Some name";
            int id = 1;
            _mockRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO { Id = id, UserName = username });
            var service = new UserService(_mockRepository.Object);

            //When
            var user = await service.GetUserById(id, default);

            //Then
            Assert.Equal(id, user.Id);
            Assert.Equal(username, user.UserName);
        }

        [Fact]
        public void GetUserByInvalidIdShouldFail()
        {
            //Given
            int id = 0;
            var service = new UserService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetUserById(id, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public void GetUserThatDoesNotExistShouldFail()
        {
            //Given
            int id = 1;
            _mockRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new UserService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetUserById(id, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.USER_NOT_FOUND, exception.Result.Message);
        }
    }
}