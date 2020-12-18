using System;

namespace Models.Comments
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public string Commenter { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}