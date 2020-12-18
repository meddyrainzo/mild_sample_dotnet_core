using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Quotes;
using Repository.Quotes;
using Services.Quotes;
using Models.Exceptions;

namespace ServicesTest.Quotes
{
    public class CreateQuoteTest
    {
        private readonly CreateQuoteDTO _dto;
        private readonly Mock<IQuoteRepository> _mockRepository;
        public CreateQuoteTest()
        {
            _dto = new CreateQuoteDTO { UserId = 1, Quote = "Some quote" };
            _mockRepository = new Mock<IQuoteRepository>();
            _mockRepository.Setup(r => r.CreateQuote(It.IsAny<CreateQuoteRequest>(), default)).ReturnsAsync(1);
        }

        [Fact]
        public async Task CreateQuoteSuccessfully()
        {
            //Given
            var service = new QuoteService(_mockRepository.Object);

            //When
            var userId = await service.CreateQuote(_dto, default);

            //Then
            Assert.Equal(1, userId);
        }

        [Fact]
        public void CreateQuoteNullDTOShouldFail()
        {
            //Given
            var service = new QuoteService(_mockRepository.Object);

            //When
            Func<Task> action = async () => await service.CreateQuote(null, default);
            var exception = Assert.ThrowsAsync<QuoterException>(action);

            //Then
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }
    }
}