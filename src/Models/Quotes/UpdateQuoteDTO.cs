namespace Models.Quotes
{
    public class UpdateQuoteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }

}