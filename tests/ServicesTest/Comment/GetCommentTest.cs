using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Comments;
using Repository.Comments;
using Services.Comments;
using Models.Exceptions;
using Repository.Quotes;
using Models.Quotes;
using System.Collections.Generic;
using System.Linq;

namespace ServicesTest.Comment
{
    public class GetCommentTest
    {
        private Mock<ICommentRepository> _mockCommentRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;

        public GetCommentTest()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
        }

        [Fact]
        public async Task GetCommentsSuccessfully()
        {
            //Given
            var quoteid = 1;
            var expected = new List<GetCommentDTO> { new GetCommentDTO(), new GetCommentDTO() };
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockCommentRepository.Setup(r => r.GetComments(It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);
            //When
            var comments = await service.GetComments(quoteid, default);
            //Then
            Assert.NotEmpty(comments);
            Assert.True(comments.Count() == 2);
        }

        [Fact]
        public void GetCommentsWithQuoteThatDoesNotExistShouldFail()
        {
            //Given
            var quoteid = 1;
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);

            // When
            Func<Task> action = async () => await service.GetComments(quoteid, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void GetCommentsWithInvalidQuoteIdShouldFail()
        {
            //Given
            var quoteid = 0;
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);

            // When
            Func<Task> action = async () => await service.GetComments(quoteid, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public async Task GetCommentsWhenNoCommentsShouldReturnEmptyList()
        {
            //Given
            var quoteid = 1;
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockCommentRepository.Setup(r => r.GetComments(It.IsAny<int>(), default)).ReturnsAsync((List<GetCommentDTO>)null);
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);
            //When
            var comments = await service.GetComments(quoteid);
            //Then
            Assert.Empty(comments);
        }
    }
}