using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Comments;
using Models.Exceptions;
using Repository.Comments;
using Repository.Quotes;

namespace Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IQuoteRepository _quoteRepository;

        public CommentService(ICommentRepository commentRepository, IQuoteRepository quoteRepository)
        {
            _commentRepository = commentRepository;
            _quoteRepository = quoteRepository;
        }

        public async Task<int> CreateComment(CreateCommentDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            await CheckIfQuoteIsValid(dto.QuoteId, token).ConfigureAwait(false);
            var request = CreateCommentRequest.CreateRequest(dto.UserId, dto.QuoteId, dto.Comment);
            return await _commentRepository.CreateComment(request, token).ConfigureAwait(false);
        }

        public async Task<IEnumerable<GetCommentDTO>> GetComments(int quoteId, CancellationToken token = default)
        {
            await CheckIfQuoteIsValid(quoteId).ConfigureAwait(false);
            var comments = await _commentRepository.GetComments(quoteId, token).ConfigureAwait(false);
            return comments ?? new List<GetCommentDTO>();
        }

        private async Task CheckIfQuoteIsValid(int quoteId, CancellationToken token = default)
        {
            quoteId.CheckInvalid();
            var quote = await _quoteRepository.GetQuote(quoteId, token).ConfigureAwait(false);
            if (quote is null)
                throw new QuoterException(ErrorReason.QUOTE_NOT_FOUND);
        }
    }
}