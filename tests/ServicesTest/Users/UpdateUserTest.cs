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
    public class UpdateUserTest
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly UpdateUserDTO _dto;

        public UpdateUserTest()
        {
            _mockRepository = new Mock<IUserRepository>();
            _dto = new UpdateUserDTO { Id = 2, UserName = "username" };
            _mockRepository.Setup(r => r.UpdateUser(It.IsAny<UpdateUserRequest>(), default)).ReturnsAsync(1);
        }

        [Fact]
        public async Task UpdateUserSuccessfully()
        {
            //Given
            var service = new UserService(_mockRepository.Object);

            //When
            var userId = await service.UpdateUser(_dto, default);

            //Then
            Assert.Equal(1, userId);
        }

        [Fact]
        public void UpdateUserThatDoesntExistShouldFail()
        {
            //Given
            _mockRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new UserService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.UpdateUser(_dto, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.USER_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void UpdateUserWithNullDTOShouldFail()
        {
            //Given
            var service = new UserService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.UpdateUser(null, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }
    }
}