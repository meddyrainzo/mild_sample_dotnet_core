using Xunit;
using Models.Likes;
using Models.Exceptions;
using System;

namespace ModelsTest.Likes
{
    public class UnlikeRequestTests
    {
        [Fact]
        public void UnlikeRequestSuccessfully()
        {
            //Given
            var userid = 1;
            var quoteid = 1;

            //When
            var request = UnlikeRequest.CreateRequest(userid, quoteid);
            //Then
            Assert.Equal(userid, request.UserId);
            Assert.Equal(quoteid, request.QuoteId);
        }

        [Fact]
        public void UnlikeRequestInvalidUserIdFails()
        {
            //Given
            var userid = 0;
            var quoteid = 1;
            Action action = () => UnlikeRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void UnlikeRequestInvalidQuoteIdFails()
        {
            //Given
            var userid = 1;
            var quoteid = 0;
            Action action = () => UnlikeRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }
    }
}