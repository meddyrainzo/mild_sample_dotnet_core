using Xunit;
using Models.Quotes;
using Models.Exceptions;
using System;

namespace ModelsTest.Quotes
{
    public class UpdateQuoteRequestTests
    {
        [Fact]
        public void UpdateQuoteRequestSuccessfully()
        {
            //Given
            var message = "This is the quote";
            var currentDate = DateTime.UtcNow;
            var userid = 2;
            var id = 1;
            //When
            var quote = UpdateQuoteRequest.CreateRequest(id, userid, message);
            //Then
            Assert.Equal(quote.UpdatedAt, currentDate, TimeSpan.FromSeconds(1));
            Assert.Equal(userid, quote.UserId);
            Assert.Equal(message, quote.Quote);
        }

        [Fact]
        public void UpdateQuoteRequestInvalidUserIdFails()
        {
            //Given
            var message = "This is the quote";
            var currentDate = DateTime.UtcNow;
            var userid = 0;
            var id = 1;

            //When

            Action action = () => UpdateQuoteRequest.CreateRequest(id, userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void UpdateQuoteRequestInvalidIdFails()
        {
            //Given
            var message = "This is the quote";
            var currentDate = DateTime.UtcNow;
            var id = 0;
            var userid = 1;

            //When

            Action action = () => UpdateQuoteRequest.CreateRequest(id, userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void UpdateQuoteRequestWithEmptyQuoteFails()
        {
            //Given
            var message = "";
            var currentDate = DateTime.UtcNow;
            var userid = 2;
            var id = 1;

            //When

            Action action = () => UpdateQuoteRequest.CreateRequest(id, userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_QUOTE, exception.Message);
        }

        [Fact]
        public void UpdateQuoteRequestWithWhitespaceQuoteFails()
        {
            //Given
            var message = "     ";
            var currentDate = DateTime.UtcNow;
            var userid = 2;
            var id = 1;

            //When

            Action action = () => UpdateQuoteRequest.CreateRequest(id, userid, message);
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_QUOTE, exception.Message);
        }
    }
}