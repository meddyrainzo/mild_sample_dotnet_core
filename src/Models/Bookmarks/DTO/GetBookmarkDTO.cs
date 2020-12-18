using System;
using Models.Quotes;

namespace Models.Bookmarks
{
    public class GetBookmarkDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public GetQuoteDetailsDTO Quote { get; set; }

    }
}