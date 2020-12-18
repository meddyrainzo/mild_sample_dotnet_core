namespace Repository.Likes
{
    public static class LikeSqlStatements
    {
        public static string like =
            @"INSERT INTO likes(userId, quoteId) VALUES (@UserId, @QuoteId);";

        public static string unlike =
            @"DELETE FROM likes WHERE userId=@UserId AND quoteId=@QuoteId;";

        public static string getLike =
            @"SELECT * FROM likes WHERE userId=@UserId AND quoteId=@QuoteId LIMIT 1;";

    }
}