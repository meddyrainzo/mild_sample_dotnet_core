using Xunit;
using Models.Quotes;
using Models.Exceptions;
using System;

namespace ModelsTest.Quotes
{
    public class CreateQuoteRequestTests
    {
        [Fact]
        public void CreateQuoteRequestSuccessfully()
        {
            //Given
            var message = "This is the quote";
            var currentDate = DateTime.UtcNow;
            var userid = 2;
            //When
            var request = CreateQuoteRequest.CreateRequest(userid, message);
            //Then
            Assert.Equal(request.CreatedAt, request.UpdatedAt);
            Assert.Equal(request.CreatedAt, currentDate, TimeSpan.FromSeconds(1));
            Assert.Equal(userid, request.UserId);
            Assert.Equal(message, request.Quote);
        }

        [Fact]
        public void CreateQuoteRequestInvalidUserIdFails()
        {
            //Given
            var message = "This is the quote";
            var currentDate = DateTime.UtcNow;
            var userid = 0;

            //When

            Action action = () => CreateQuoteRequest.CreateRequest(userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void CreateQuoteRequestEmptyQuoteFails()
        {
            //Given
            var message = "";
            var currentDate = DateTime.UtcNow;
            var userid = 2;

            //When

            Action action = () => CreateQuoteRequest.CreateRequest(userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_QUOTE, exception.Message);
        }

        [Fact]
        public void CreateQuoteRequestWhitespaceQuoteFails()
        {
            //Given
            var message = "     ";
            var currentDate = DateTime.UtcNow;
            var userid = 2;

            //When

            Action action = () => CreateQuoteRequest.CreateRequest(userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_QUOTE, exception.Message);
        }
    }
}