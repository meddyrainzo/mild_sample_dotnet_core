using Xunit;
using Models.Quotes;
using Models.Exceptions;
using System;

namespace ModelsTest.Quotes
{
    public class DeleteQuoteRequestTests
    {
        [Fact]
        public void DeleteQuoteRequestSuccesful()
        {
            //Given
            var id = 1;
            var userid = 1;
            //When
            var request = DeleteQuoteRequest.CreateRequest(id, userid);

            //Then
            Assert.Equal(id, request.Id);
            Assert.Equal(userid, request.UserId);
        }

        [Fact]
        public void DeleteQuoteRequestInvalidIdFails()
        {
            //Given
            var id = 0;
            var userid = 1;
            Action action = () => DeleteQuoteRequest.CreateRequest(id, userid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void DeleteQuoteRequestInvalidUserIdFails()
        {
            //Given
            var id = 1;
            var userid = 0;
            Action action = () => DeleteQuoteRequest.CreateRequest(id, userid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }
    }
}