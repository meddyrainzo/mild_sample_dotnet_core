using Xunit;
using Models.Likes;
using Models.Exceptions;
using System;

namespace ModelsTest.Likes
{
    public class CreateLikeRequestTests
    {
        [Fact]
        public void CreateLikeRequestSuccessfully()
        {
            //Given
            var userid = 1;
            var quoteid = 1;

            //When
            var request = CreateLikeRequest.CreateRequest(userid, quoteid);
            //Then
            Assert.Equal(userid, request.UserId);
            Assert.Equal(quoteid, request.QuoteId);
        }

        [Fact]
        public void CreateLikeRequestInvalidUserIdFails()
        {
            //Given
            var userid = 0;
            var quoteid = 1;
            Action action = () => CreateLikeRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void CreateLikeRequestInvalidQuoteIdFails()
        {
            //Given
            var userid = 1;
            var quoteid = 0;
            Action action = () => CreateLikeRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }
    }
}