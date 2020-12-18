using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Bookmarks;
using Repository.Bookmarks;
using Services.Bookmarks;
using Models.Exceptions;
using Repository.Quotes;
using System.Collections.Generic;
using System.Linq;
using Repository.Users;
using Models.Users;

namespace ServicesTest.Bookmark
{
    public class GetBookmarkTest
    {
        private Mock<IBookmarkRepository> _mockBookmarkRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;
        private Mock<IUserRepository> _mockUserRepository;

        public GetBookmarkTest()
        {
            _mockBookmarkRepository = new Mock<IBookmarkRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task GetBookmarksSuccessfully()
        {
            //Given
            var userid = 1;
            var expected = new List<GetBookmarkDTO> { new GetBookmarkDTO(), new GetBookmarkDTO() };
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            _mockBookmarkRepository.Setup(r => r.GetBookmarks(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var comments = await service.GetBookmarks(userid, 1, 10, default);
            //Then
            Assert.NotEmpty(comments);
            Assert.True(comments.Count() == 2);
        }

        [Fact]
        public async Task GetBookmarksNegativeSkipShouldBeChangedToAbsValueSuccessfully()
        {
            //Given
            var userid = 1;
            var expected = new List<GetBookmarkDTO> { new GetBookmarkDTO(), new GetBookmarkDTO() };
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            _mockBookmarkRepository.Setup(r => r.GetBookmarks(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var comments = await service.GetBookmarks(userid, -1, 10, default);
            //Then
            Assert.NotEmpty(comments);
            Assert.True(comments.Count() == 2);
        }

        [Fact]
        public async Task GetBookmarksWithNegativeLimitShouldBeChangedToAbsValueSuccessfully()
        {
            //Given
            var userid = 1;
            var expected = new List<GetBookmarkDTO> { new GetBookmarkDTO() };
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            _mockBookmarkRepository.Setup(r => r.GetBookmarks(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync(expected);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var comments = await service.GetBookmarks(userid, 1, -10, default);
            //Then
            Assert.NotEmpty(comments);
            Assert.Single(comments);
        }

        [Fact]
        public void GetBookmarksWithInvalidUserIdShouldFail()
        {
            //Given
            var userid = 0;
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.GetBookmarks(userid, 1, 10, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.INVALID_ID, exception.Result.Message);
        }

        [Fact]
        public void GetBookmarksWithUserThatDoesNotExistShouldFail()
        {
            //Given
            var userid = 1;
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);

            // When
            Func<Task> action = async () => await service.GetBookmarks(userid, 1, 10, default);

            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.USER_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public async Task GetBookmarksWhenNoBookmarksShouldReturnEmptyList()
        {
            //Given
            var userid = 1;
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            _mockBookmarkRepository.Setup(r => r.GetBookmarks(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), default)).ReturnsAsync((List<GetBookmarkDTO>)null);
            var service = new BookmarkService(_mockBookmarkRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var comments = await service.GetBookmarks(userid, 1, 10, default);
            //Then
            Assert.Empty(comments);
        }
    }
}