namespace Models.Comments
{
    public class CreateCommentDTO
    {
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
    }
}