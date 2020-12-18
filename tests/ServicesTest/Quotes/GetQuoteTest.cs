using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;

using Models.Quotes;
using Repository.Quotes;
using Services.Quotes;
using Models.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ServicesTest.Quotes
{
    public class GetQuoteTest
    {
        private readonly Mock<IQuoteRepository> _mockRepository;
        public GetQuoteTest()
        {
            _mockRepository = new Mock<IQuoteRepository>();
        }

        [Fact]
        public async Task GetQuoteSuccessfully()
        {
            //Given
            int id = 1;
            int userId = 1;
            var expected = new GetQuoteDetailsDTO { Id = 1 };
            _mockRepository.Setup(r => r.GetQuoteDetails(It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var quote = await service.GetQuote(id, userId, default);
            //Then
            Assert.Equal(id, quote.Id);
        }

        [Fact]
        public void GetQuoteInvalidIdFails()
        {
            //Given
            int id = 0;
            int userId = 1;
            var service = new QuoteService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetQuote(id, userId, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public void GetQuoteInvalidUserIdFails()
        {
            //Given
            int id = 1;
            int userId = 0;
            var service = new QuoteService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetQuote(id, userId, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public void GetQuoteThatDoesNotExistShouldFail()
        {
            //Given
            int id = 1;
            int userId = 1;
            _mockRepository.Setup(r => r.GetQuoteDetails(It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDetailsDTO)null);
            var service = new QuoteService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetQuote(id, userId, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public async Task GetQuotesSuccessfully()
        {
            //Given
            var expected = new List<GetQuoteDetailsDTO> { new GetQuoteDetailsDTO { Id = 1 } };
            _mockRepository.Setup(r => r.GetQuotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var quotes = await service.GetQuotes(1, 10, default);
            //Then
            Assert.Single(quotes);
        }

        [Fact]
        public async Task GetQuotesNegativeLimitShouldBeChangedToAbsValueSuccessfully()
        {
            //Given
            var expected = new List<GetQuoteDetailsDTO> { new GetQuoteDetailsDTO { Id = 1 } };
            _mockRepository.Setup(r => r.GetQuotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var quotes = await service.GetQuotes(1, -10, default);
            //Then
            Assert.Single(quotes);
        }

        [Fact]
        public async Task GetQuotesNegativeSkipShouldBeChangedToAbsValueSuccessfully()
        {
            //Given
            int userId = 1;
            var expected = new List<GetQuoteDetailsDTO> { new GetQuoteDetailsDTO { Id = 1 }, new GetQuoteDetailsDTO { Id = 1 } };
            _mockRepository.Setup(r => r.GetQuotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new QuoteService(_mockRepository.Object);
            //When
            var quotes = await service.GetQuotes(userId, -1, 10, default);
            //Then
            Assert.True(quotes.Count() == 2);
        }

        [Fact]
        public async Task GetQuotesThatDoesNotExistShouldReturnEmptyList()
        {
            //Given
            _mockRepository.Setup(r => r.GetQuotes(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync((List<GetQuoteDetailsDTO>)null);
            var service = new QuoteService(_mockRepository.Object);

            //When
            var quotes = await service.GetQuotes(1, 10, default);

            //Then
            Assert.NotNull(quotes);
            Assert.Empty(quotes);
        }

        [Fact]
        public void GetQuotesWithInvalidUserIdShouldFail()
        {
            //Given
            int userId = 0;
            var service = new QuoteService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.GetQuotes(userId);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }
    }
}