using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Bookmarks;
using Models.Exceptions;
using Repository.Bookmarks;
using Repository.Quotes;
using Repository.Users;

namespace Services.Bookmarks
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        private readonly IQuoteRepository _quoteRepository;
        private readonly IUserRepository _userRepository;

        public BookmarkService(IBookmarkRepository bookmarkRepository, IQuoteRepository quoteRepository, IUserRepository userRepository)
        {
            _bookmarkRepository = bookmarkRepository;
            _quoteRepository = quoteRepository;
            _userRepository = userRepository;
        }

        public async Task<int> CreateBookmark(CreateBookmarkDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            var quote = await _quoteRepository.GetQuote(dto.QuoteId, token).ConfigureAwait(false);
            if (quote is null)
                throw new QuoterException(ErrorReason.QUOTE_NOT_FOUND);
            var user = await _userRepository.GetUserById(dto.UserId, token).ConfigureAwait(false);
            if (user is null)
                throw new QuoterException(ErrorReason.USER_NOT_FOUND);
            var request = CreateBookmarkRequest.CreateRequest(dto.UserId, dto.QuoteId);
            return await _bookmarkRepository.CreateBookmark(request, token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<GetBookmarkDTO>> GetBookmarks(int userid, int skip = 0, int limit = 10, CancellationToken token = default)
        {
            userid.CheckInvalid();
            var user = await _userRepository.GetUserById(userid, token).ConfigureAwait(false);
            if (user is null)
                throw new QuoterException(ErrorReason.USER_NOT_FOUND);
            var bookmarks = await _bookmarkRepository.GetBookmarks(userid, Math.Abs(skip), Math.Abs(limit), token).ConfigureAwait(false);
            return bookmarks ?? new List<GetBookmarkDTO>();
        }

        public async Task<int> RemoveBookmark(int id, CancellationToken token = default)
        {
            id.CheckInvalid();
            var bookmark = await _bookmarkRepository.GetBookmarkById(id, token).ConfigureAwait(false);
            if (bookmark == null)
                throw new QuoterException(ErrorReason.BOOKMARK_NOT_FOUND);
            return await _bookmarkRepository.RemoveBookmark(id, token).ConfigureAwait(false);
        }
    }
}