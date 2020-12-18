using Xunit;
using Models.Comments;
using Models.Exceptions;
using System;

namespace ModelsTest.Comments
{
    public class CommentRequestTests
    {
        [Fact]
        public void CreateCommentRequestSuccessfully()
        {
            //Given
            var userid = 1;
            var quoteid = 1;
            var comment = "Some comment";
            var currentDate = DateTime.UtcNow;

            //When
            var request = CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //Then
            Assert.Equal(userid, request.UserId);
            Assert.Equal(quoteid, request.QuoteId);
            Assert.Equal(currentDate, request.CreatedAt, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void CreateCommentRequestInvalidUserIdFails()
        {
            //Given
            var userid = 0;
            var quoteid = 1;
            var comment = "Some comment";
            Action action = () => CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void CreateCommentRequestInvalidQuoteIdFails()
        {
            //Given
            var userid = 1;
            var quoteid = 0;
            var comment = "Some comment";
            Action action = () => CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Message);
        }

        [Fact]
        public void CreateCommentRequestEmptyCommentFails()
        {
            //Given
            var userid = 1;
            var quoteid = 1;
            var comment = "";
            Action action = () => CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_COMMENT, exception.Message);
        }

        [Fact]
        public void CreateCommentRequestWhitespaceCommentFails()
        {
            //Given
            var userid = 1;
            var quoteid = 1;
            var comment = "     ";
            Action action = () => CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.EMPTY_COMMENT, exception.Message);
        }

        [Fact]
        public void CreateCommentRequestAllFieldInvalidFails()
        {
            //Given
            var userid = 0;
            var quoteid = 0;
            var comment = "";
            var expectedError = $"{ErrorReason.INVALID_ID}, {ErrorReason.INVALID_ID}, {ErrorReason.EMPTY_COMMENT}";
            Action action = () => CreateCommentRequest.CreateRequest(userid, quoteid, comment);
            //When
            var exception = Assert.Throws<QuoterException>(action);
            //Then
            Assert.Equal(expectedError, exception.Message);
        }
    }
}