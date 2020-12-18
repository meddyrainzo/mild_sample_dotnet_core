using System;
using Models.Exceptions;

namespace Models.Comments
{
    public class CreateCommentRequest
    {
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        private CreateCommentRequest(int userid, int quoteid, string comment)
        {
            UserId = userid;
            QuoteId = quoteid;
            Comment = comment;
        }

        public static CreateCommentRequest CreateRequest(int userid, int quoteid, string comment)
        {
            var request = new CreateCommentRequest(userid, quoteid, comment);
            var validator = new CreateCommentValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }
    }
}