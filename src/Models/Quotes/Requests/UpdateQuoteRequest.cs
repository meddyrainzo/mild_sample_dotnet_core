using System;
using Models.Exceptions;

namespace Models.Quotes
{
    public class UpdateQuoteRequest
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public string Quote { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private UpdateQuoteRequest(int id, int userId, string quote)
        {
            Id = id;
            UserId = userId;
            Quote = quote;
        }

        public static UpdateQuoteRequest CreateRequest(int id, int userid, string quote)
        {
            var request = new UpdateQuoteRequest(id, userid, quote);
            var validator = new UpdateQuoteValidator();
            var validationResult = validator.Validate(request);
            return validationResult.IsValid ? request : throw new QuoterException(String.Join(", ", validationResult.Errors));
        }

    }

}