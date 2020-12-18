using System;
using Models.Exceptions;

namespace Models.Quotes
{
    public class CreateQuoteRequest
    {
        public int UserId { get; private set; }
        public string Quote { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private CreateQuoteRequest(int userid, string quote) =>
            (UserId, Quote) = (userid, quote);

        public static CreateQuoteRequest CreateRequest(int userid, string quote)
        {
            var request = new CreateQuoteRequest(userid, quote);
            var validator = new CreateQuoteValidator();
            var validationResult = validator.Validate(request);

            return validationResult.IsValid ? request : throw new QuoterException(String.Join(", ", validationResult.Errors));
        }
    }

}