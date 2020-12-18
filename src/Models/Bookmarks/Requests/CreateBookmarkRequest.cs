using System;
using Models.Exceptions;

namespace Models.Bookmarks
{
    public class CreateBookmarkRequest
    {
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private CreateBookmarkRequest(int userid, int quoteid)
        {
            UserId = userid;
            QuoteId = quoteid;
        }

        public static CreateBookmarkRequest CreateRequest(int userid, int quoteid)
        {
            var request = new CreateBookmarkRequest(userid, quoteid);
            var validator = new CreateBookmarkValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }
    }
}