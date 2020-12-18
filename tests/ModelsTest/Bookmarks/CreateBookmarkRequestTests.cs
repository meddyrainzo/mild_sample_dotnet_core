using Xunit;
using Models.Bookmarks;
using Models.Exceptions;
using System;

namespace ModelsTest.Bookmarks
{
    public class CreateBookmarkRequestTests
    {
        [Fact]
        public void CreateBookmarkRequestSuccessfully()
        {
            //Given
            var userid = 1;
            var quoteid = 1;

            //When
            var request = CreateBookmarkRequest.CreateRequest(userid, quoteid);
            //Then
            Assert.Equal(userid, request.UserId);
            Assert.Equal(quoteid, request.QuoteId);
        }

        [Fact]
        public void CreateBookmarkRequestInvalidUserIdFails()
        {
            //Given
            var userid = 0;
            var quoteid = 1;
            Action action = () => CreateBookmarkRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void CreateBookmarkRequestInvalidQuoteIdFails()
        {
            //Given
            var userid = 1;
            var quoteid = 0;
            Action action = () => CreateBookmarkRequest.CreateRequest(userid, quoteid);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }
    }
}