using System;
using Models.Exceptions;
using Models.Users;
using Xunit;

namespace ModelsTest.Users
{
    public class CreateUserRequestTests
    {
        [Fact]
        public void CreateUserRequestWithUsernameSuccessfully()
        {
            // Given
            var username = "Username";
            // When
            var request = CreateUserRequest.CreateRequest(username);
            // Then
            Assert.Equal(request.UserName, username);
        }

        [Fact]
        public void CreateUserRequestWithEmptyUsernameFails()
        {
            // Given
            var username = "";
            // When
            Action action = () => CreateUserRequest.CreateRequest(username);
            var exception = Assert.Throws<QuoterException>(action);
            // Then
            Assert.Equal(ErrorReason.EMPTY_USERNAME, exception.Message);
        }

        [Fact]
        public void CreateUserRequestWithWhiteSpaceUsernameOnlyFails()
        {
            // Given
            var username = "     ";
            //When 
            Action action = () => CreateUserRequest.CreateRequest(username);
            var exception = Assert.Throws<QuoterException>(action);
            // Then
            Assert.Equal(ErrorReason.EMPTY_USERNAME, exception.Message);
        }

        [Fact]
        public void CreatedUserRequestHasSameCreatedAndUpdatedDate()
        {
            // Given 
            var username = "createdUser";
            var currentDate = DateTime.UtcNow;
            //  When
            var request = CreateUserRequest.CreateRequest(username);
            // Then 
            Assert.IsType<DateTime>(request.CreatedAt);
            Assert.IsType<DateTime>(request.UpdatedAt);
            Assert.Equal(request.CreatedAt, request.UpdatedAt);
            Assert.Equal(request.CreatedAt, currentDate, TimeSpan.FromSeconds(1));
        }
    }


}