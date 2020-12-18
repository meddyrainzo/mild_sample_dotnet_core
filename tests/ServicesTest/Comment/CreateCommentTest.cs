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

namespace ServicesTest.Comment
{
    public class CreateCommentTest
    {
        private readonly CreateCommentDTO _dto;
        private Mock<ICommentRepository> _mockCommentRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;

        public CreateCommentTest()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _dto = new CreateCommentDTO { UserId = 1, QuoteId = 1, Comment = "Some comment" };
        }

        [Fact]
        public async Task CreateCommentSuccessfully()
        {
            //Given
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockCommentRepository.Setup(r => r.CreateComment(It.IsAny<CreateCommentRequest>(), default)).ReturnsAsync(1);
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);
            //When
            var created = await service.CreateComment(_dto, default);
            //Then
            Assert.Equal(1, created);
        }

        [Fact]
        public void CreateCommentWithQuoteThatDoesntExistShouldFail()
        {
            //Given
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateComment(_dto, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void CreateCommentWithNullDtoShouldFail()
        {
            //Given
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateComment(null, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }

        [Fact]
        public void CreateCommentWithEmptyMessageShouldFail()
        {
            //Given
            _dto.Comment = "";
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            var service = new CommentService(_mockCommentRepository.Object, _mockQuoteRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateComment(_dto, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_COMMENT, exception.Result.Message);
        }
    }
}