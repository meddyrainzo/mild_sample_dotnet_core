using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Models.Exceptions;
using Models.Quotes;
using Repository.Quotes;

namespace Services.Quotes
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _repository;

        public QuoteService(IQuoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateQuote(CreateQuoteDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            var request = CreateQuoteRequest.CreateRequest(dto.UserId, dto.Quote);
            return await _repository.CreateQuote(request, token).ConfigureAwait(false);
        }

        public async Task<int> DeleteQuote(int quoteId, CancellationToken token = default)
        {
            quoteId.CheckInvalid();
            var quote = await GetQuote(quoteId, token).ConfigureAwait(false);
            return await _repository.DeleteQuote(quoteId, token).ConfigureAwait(false);
        }

        public async Task<GetQuoteDetailsDTO> GetQuote(int id, int userId, CancellationToken token = default)
        {
            id.CheckInvalid();
            userId.CheckInvalid();
            var quote = await _repository.GetQuoteDetails(id, userId, token).ConfigureAwait(false);
            return quote ?? throw new QuoterException(ErrorReason.QUOTE_NOT_FOUND);
        }

        public async Task<IEnumerable<GetQuoteDetailsDTO>> GetQuotes(int userId, int skip = 0, int limit = 10, CancellationToken token = default)
        {
            userId.CheckInvalid();
            var quotes = await _repository.GetQuotes(userId, Math.Abs(skip), Math.Abs(limit), token).ConfigureAwait(false);
            return quotes ?? new List<GetQuoteDetailsDTO>();
        }

        public async Task<int> UpdateQuote(UpdateQuoteDTO dto, CancellationToken token = default)
        {
            dto.CheckNull();
            var request = UpdateQuoteRequest.CreateRequest(dto.Id, dto.UserId, dto.Message);
            var quote = await GetQuote(request.Id, token).ConfigureAwait(false);
            if (!(quote.UserId == request.UserId))
                throw new QuoterException(ErrorReason.NOT_PERMITTED);
            return await _repository.UpdateQuote(request, token).ConfigureAwait(false);
        }

        private async Task<GetQuoteDTO> GetQuote(int quoteId, CancellationToken token = default)
        {
            return await _repository.GetQuote(quoteId, token).ConfigureAwait(false) ?? throw new QuoterException(ErrorReason.QUOTE_NOT_FOUND);
        }
    }
}