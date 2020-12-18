using Xunit;
using System;
using System.Threading.Tasks;
using Moq;

using Models.Likes;
using Repository.Likes;
using Services.Likes;
using Models.Exceptions;
using Repository.Quotes;
using Models.Quotes;
using Repository.Users;
using Models.Users;

namespace ServicesTest.Likes
{
    public class LikeTest
    {
        private readonly CreateLikeDTO _dto;
        private Mock<ILikeRepository> _mockLikeRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;
        private Mock<IUserRepository> _mockUserRepository;

        public LikeTest()
        {
            _mockLikeRepository = new Mock<ILikeRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _dto = new CreateLikeDTO { UserId = 1, QuoteId = 1 };
        }

        [Fact]
        public async Task LikeQuoteSuccessfully()
        {
            //Given
            var expected = 1;
            _mockLikeRepository.Setup(r => r.Like(It.IsAny<CreateLikeRequest>(), default)).ReturnsAsync(1);
            _mockLikeRepository.Setup(r => r.GetLike(It.IsAny<GetLikeRequest>(), default)).ReturnsAsync((GetLikeDTO)null);
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            var service = new LikeService(_mockLikeRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var liked = await service.LikeOrUnlike(_dto, default);

            //Then
            Assert.Equal(expected, liked);
        }

        [Fact]
        public async Task UnlikeQuoteSuccessfully()
        {
            //Given
            var expected = 1;
            _mockLikeRepository.Setup(r => r.Unlike(It.IsAny<UnlikeRequest>(), default)).ReturnsAsync(1);
            _mockLikeRepository.Setup(r => r.GetLike(It.IsAny<GetLikeRequest>(), default)).ReturnsAsync(new GetLikeDTO());
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync(new GetUserDTO());
            var service = new LikeService(_mockLikeRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            var liked = await service.LikeOrUnlike(_dto, default);

            //Then
            Assert.Equal(expected, liked);
        }

        [Fact]
        public void LikeQuoteNullDtoShouldFail()
        {
            //Given
            var service = new LikeService(_mockLikeRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            Func<Task> action = async () => await service.LikeOrUnlike(null);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.EMPTY_DATA_SENT, exception.Result.Message);
        }

        [Fact]
        public void LikeQuoteThatDoesNotExistShouldFail()
        {
            //Given
            _mockLikeRepository.Setup(r => r.GetLike(It.IsAny<GetLikeRequest>(), default)).ReturnsAsync(new GetLikeDTO());
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync((GetQuoteDTO)null);
            var service = new LikeService(_mockLikeRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            Func<Task> action = async () => await service.LikeOrUnlike(_dto, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.QUOTE_NOT_FOUND, exception.Result.Message);
        }

        [Fact]
        public void LikeQuoteWithUserThatDoesNotExistShouldFail()
        {
            //Given
            _mockLikeRepository.Setup(r => r.GetLike(It.IsAny<GetLikeRequest>(), default)).ReturnsAsync(new GetLikeDTO());
            _mockQuoteRepository.Setup(r => r.GetQuote(It.IsAny<int>(), default)).ReturnsAsync(new GetQuoteDTO());
            _mockUserRepository.Setup(r => r.GetUserById(It.IsAny<int>(), default)).ReturnsAsync((GetUserDTO)null);
            var service = new LikeService(_mockLikeRepository.Object, _mockQuoteRepository.Object, _mockUserRepository.Object);
            //When
            Func<Task> action = async () => await service.LikeOrUnlike(_dto, default);
            //Then
            var exception = Assert.ThrowsAsync<QuoterException>(action);
            Assert.Equal(ErrorReason.USER_NOT_FOUND, exception.Result.Message);
        }
    }
}