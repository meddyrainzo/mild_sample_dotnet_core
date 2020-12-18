using Xunit;
using System.Threading.Tasks;
using Moq;

using Models.Quotes;
using Repository.Quotes;
using Services.Quotes;
using System;
using Models.Exceptions;

namespace ServicesTest.Quotes
{
    public class UpdateQuoteTest
    {
        private readonly UpdateQuoteDTO _dto;
        private readonly Mock<IQuoteRepository> _mockRepository;

        public UpdateQuoteTest()
        {
            _mockRepository = new Mock<IQuoteRepository>();
            _dto = new UpdateQuoteDTO { Id = 1, UserId = 1, Message = "Some message" };
        }

        [Fact]
        public async Task UpdateQuoteSuccessfully()
        {
            //Given
            _mockRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO { Id = 1, UserId = 1 });
            _mockRepository.Setup(r => r.UpdateQuote(It.IsAny<UpdateQuoteRequest>(), default)).ReturnsAsync(1);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var updateQuote = await service.UpdateQuote(_dto, default);
            //Then
            Assert.Equal(1, updateQuote);
        }

        [Fact]
        public void UpdateQuoteNullDTOSholdFail()
        {
            //Given
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.UpdateQuote(null, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }

        [Fact]
        public void UpdateQuoteNotPermittedShouldFail()
        {
            //Given
            _mockRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO { Id = 1, UserId = 2 });
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.UpdateQuote(_dto, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.NOT_PERMITTED, exception.Result.Message);
        }

        [Fact]
        public void UpdateQuoteNotExistShouldFail()
        {
            //Given
            _mockRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.UpdateQuote(_dto, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void UpdateQuoteEmptyMessageShouldFail()
        {
            //Given
            _dto.Message = "";
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.UpdateQuote(_dto, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_QUOTE, exception.Result.Message);
        }

    }
}