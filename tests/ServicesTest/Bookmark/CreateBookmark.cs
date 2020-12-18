using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Bookmarks;
using Repository.Bookmarks;
using Services.Bookmarks;
using Models.Exceptions;
using Repository.Quotes;
using Models.Quotes;
using Repository.Users;
using Models.Users;

namespace ServicesTest.Bookmark
{
    public class CreateBookmarkTest
    {
        private readonly CreateBookmarkDTO _dto;
        private Mock<IBookmarkRepository> _mockBookmarkRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;
        private Mock<IUserRepository> _mockUserRepository;

        public CreateBookmarkTest()
        {
            _mockBookmarkRepository = new Mock<IBookmarkRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _dto = new CreateBookmarkDTO { UserId = 1, QuoteId = 1 };
        }

        [Fact]
        public async Task CreateBookmarkSuccessfully()
        {
            //Given
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockBookmarkRepository.Setup(r => r.CreateBookmark(It.IsAny<CreateBookmarkRequest>(), default)).ReturnsAsync(1);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var created = await service.CreateBookmark(_dto, default);
            //Then
            Assert.Equal(1, created);
        }

        [Fact]
        public void CreateBookmarkWithQuoteThatDoesntExistShouldFail()
        {
            //Given
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateBookmark(_dto, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void CreateBookmarkWithUserThatDoesntExistShouldFail()
        {
            //Given
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateBookmark(_dto, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.USER_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void CreateBookmarkWithNullDtoShouldFail()
        {
            //Given
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.CreateBookmark(null, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }
    }
}