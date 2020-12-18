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
    public class DeleteQuoteTest
    {
        private readonly Mock<IQuoteRepository> _mockRepository;

        public DeleteQuoteTest()
        {
            _mockRepository = new Mock<IQuoteRepository>();
        }

        [Fact]
        public async Task DeleteQuoteSuccessfully()
        {
            //Given
            int id = 1;
            _mockRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockRepository.Setup(r => r.DeleteQuote(It.IsAny<int>(), default)).ReturnsAsync(1);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var deleteQuote = await service.DeleteQuote(id, default);
            //Then
            Assert.Equal(1, deleteQuote);
        }

        [Fact]
        public void DeleteQuoteInvalidIdShouldFail()
        {
            //Given
            int id = 0;
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.DeleteQuote(id, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public void DeleteQuoteThatDoesntExistShouldFail()
        {
            //Given
            int id = 1;
            _mockRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new QuoteService(_mockRepository.Object);
            //When
            Func<Task> action = async () => await service.DeleteQuote(id, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            //Then
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }
    }
}