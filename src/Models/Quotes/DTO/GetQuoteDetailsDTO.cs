namespace Models.Quotes
{
    public class GetQuoteDetailsDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Quote { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfComments { get; set; }
        public int NumberOfBookmarks { get; set; }

        public int LikedByYou { get; set; }
        public int BookmarkedByYou { get; set; }

    }

}