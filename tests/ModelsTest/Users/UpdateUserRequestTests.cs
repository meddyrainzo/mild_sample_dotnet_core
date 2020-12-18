
using Xunit;
using System;
using Models.Exceptions;
using Models.Users;

namespace ModelsTest.Users
{
    public class UpdateUserRequestTests
    {
        [Fact]
        public void UpdateUserRequestSuccessfully()
        {
            // Given
            var id = 1;
            var username = "username";
            var currentDate = DateTime.UtcNow;
            // When
            var request = UpdateUserRequest.CreateRequest(id, username);
            // Then
            Assert.Equal(id, request.Id);
            Assert.Equal(username, request.UserName);
            Assert.Equal(request.UpdatedAt, currentDate, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void UpdateUserRequestWithInvalidIdFails()
        {
            //Given
            var id = 0;
            var username = "username";
            //When
            Action action = () => UpdateUserRequest.CreateRequest(id, username);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void UpdateUserRequestWithEmptyUsernameFails()
        {
            //Given
            var id = 1;
            var username = "";
            //When
            Action action = () => UpdateUserRequest.CreateRequest(id, username);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_USERNAME, exception.Message);
        }

        [Fact]
        public void UpdateUserRequestWithWhitespaceUsernameFails()
        {
            //Given
            var id = 1;
            var username = "   ";
            //When
            Action action = () => UpdateUserRequest.CreateRequest(id, username);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_USERNAME, exception.Message);
        }

        [Fact]
        public void UpdateUserRequestWithInvalidUsernameAndIdFails()
        {
            //Given
            var id = 0;
            var username = "   ";
            var expectedError = $"{ErrorReason.INVALID_ID}, {ErrorReason.EMPTY_USERNAME}";
            //When
            Action action = () => UpdateUserRequest.CreateRequest(id, username);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(expectedError, exception.Message);
        }

    }

}