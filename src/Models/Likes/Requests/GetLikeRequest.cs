using Models.Exceptions;

namespace Models.Likes
{
    public class GetLikeRequest
    {
        public int UserId { get; private set; }
        public int QuoteId { get; private set; }

        private GetLikeRequest(int userId, int quoteId)
        {
            UserId = userId;
            QuoteId = quoteId;
        }

        public static GetLikeRequest CreateRequest(int userid, int quoteid)
        {
            var request = new GetLikeRequest(userid, quoteid);
            var validator = new GetLikeValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }
    }
}