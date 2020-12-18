using Models.Exceptions;

namespace Models.Likes
{
    public class CreateLikeRequest
    {
        public int UserId { get; private set; }
        public int QuoteId { get; private set; }

        private CreateLikeRequest(int userId, int quoteId)
        {
            UserId = userId;
            QuoteId = quoteId;
        }

        public static CreateLikeRequest CreateRequest(int userid, int quoteid)
        {
            var request = new CreateLikeRequest(userid, quoteid);
            var validator = new CreateLikeValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }
    }
}