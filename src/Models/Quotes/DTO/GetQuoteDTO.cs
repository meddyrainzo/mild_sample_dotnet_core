using System;

namespace Models.Quotes
{

    public class GetQuoteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Quote { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

}