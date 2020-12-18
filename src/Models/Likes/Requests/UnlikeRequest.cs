using Models.Exceptions;

namespace Models.Likes
{
    public class UnlikeRequest
    {
        public int UserId { get; private set; }
        public int QuoteId { get; private set; }

        private UnlikeRequest(int userId, int quoteId)
        {
            UserId = userId;
            QuoteId = quoteId;
        }

        public static UnlikeRequest CreateRequest(int userid, int quoteid)
        {
            var request = new UnlikeRequest(userid, quoteid);
            var validator = new UnlikeValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }
    }
}