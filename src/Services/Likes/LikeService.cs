using System.Threading;
using System.Threading.Tasks;
using Models.Exceptions;
using Models.Likes;
using Repository.Likes;
using Repository.Quotes;
using Repository.Users;

namespace Services.Likes
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IUserRepository _userRepository;

        public LikeService(ILikeRepository repository, IQuoteRepository quoteRepository,
            IUserRepository userRepository)
        {
            _likeRepository = repository;
            _quoteRepository = quoteRepository;
            _userRepository = userRepository;
        }

        public async Task<int> LikeOrUnlike(CreateLikeDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();

            var quote = await _quoteRepository.GetQuote(dto.QuoteId, token).ConfigureAwait(false);
            if (quote is null)
                throw new QuoterException(ErrorReason.QUOTE_NOT_FOUND);

            var user = await _userRepository.GetUserById(dto.UserId, token).ConfigureAwait(false);
            if (user is null)
                throw new QuoterException(ErrorReason.USER_NOT_FOUND);

            // Unlike if you have liked already
            var like = await _likeRepository.GetLike(GetLikeRequest.CreateRequest(dto.UserId, dto.QuoteId), token).ConfigureAwait(false);
            if (like != null)
                return await Unlike(dto, token).ConfigureAwait(false);

            var request = CreateLikeRequest.CreateRequest(dto.UserId, dto.QuoteId);
            return await _likeRepository.Like(request, default).ConfigureAwait(false);
        }

        private async Task<int> Unlike(CreateLikeDTO dto, CancellationToken token = default)
        {
            var request = UnlikeRequest.CreateRequest(dto.UserId, dto.QuoteId);
            return await _likeRepository.Unlike(request, default).ConfigureAwait(false);

        }
    }
}