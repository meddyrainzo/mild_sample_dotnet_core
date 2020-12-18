using Xunit;
using System;
using System.Threading.Tasks;
using Moq;
using Repository.Bookmarks;
using Services.Bookmarks;
using Models.Exceptions;
using Repository.Quotes;
using Models.Bookmarks;
using Repository.Users;

namespace ServicesTest.Bookmark
{
    public class RemoveBookmarkTest
    {
        private Mock<IBookmarkRepository> _mockBookmarkRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;
        private Mock<IUserRepository> _mockUserRepository;

        public RemoveBookmarkTest()
        {
            _mockBookmarkRepository = new Mock<IBookmarkRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task RemoveBookmarkSuccessfully()
        {
            //Given
            var id = 1;
            _mockBookmarkRepository.Setup(r => r.GetBookmarkById(It.IsAny<int>(), default)).ReturnsAsync(new GetBookmarkDTO());
            _mockBookmarkRepository.Setup(r => r.RemoveBookmark(It.IsAny<int>(), default)).ReturnsAsync(1);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var created = await service.RemoveBookmark(id, default);
            //Then
            Assert.Equal(1, created);
        }

        [Fact]
        public void RemoveBookmarkThatDoesntExistShouldFail()
        {
            var id = 1;
            _mockBookmarkRepository.Setup(r => r.GetBookmarkById(It.IsAny<int>(), default)).ReturnsAsync((GetBookmarkDTO)null);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.RemoveBookmark(id, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.BOOKMARK_NOT_FOUND, exception.Result.Message);
        }


        [Fact]
        public void RemoveBookmarkWithInvalidIdShouldFail()
        {
            //Given
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.RemoveBookmark(0, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }
    }
}