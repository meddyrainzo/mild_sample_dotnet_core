namespace Repository.Quotes
{
    public static class QuoteSqlStatements
    {
        private static string getQuoteDetailsBaseStatement =
        @"SELECT q.id, q.userId, q.quote, q.createdAt, q.updatedAt,
             u.username, (SELECT COUNT(quoteId) FROM likes WHERE quoteId=q.id) as numberOfLikes, 
			 (SELECT COUNT(quoteId) FROM comments WHERE quoteId=q.id) as numberOfComments,
             (SELECT COUNT(quoteId) FROM bookmarks WHERE quoteId=q.id),
             (SELECT COUNT(1) FROM likes WHERE quoteId=q.id AND userId=@userId) AS likedByYou,
             (SELECT COUNT(1) FROM bookmarks WHERE quoteId=q.id AND userId=@userId) AS boomarkedByYou
             FROM quotes as q INNER JOIN users as u ON u.id=q.userId";
        public static string getQuotes =
            $"{getQuoteDetailsBaseStatement} OFFSET @skip LIMIT @limit;";

        public static string getSingleQuoteById =
            @"SELECT * FROM quotes WHERE id=@id ";
        public static string getSingleQuoteDetails =
            $"{getQuoteDetailsBaseStatement} WHERE q.id=@id;";
        public static string createQuote =
            @"INSERT INTO quotes(userId, quote, createdAt, updatedAt)
            VALUES(@UserId, @Quote, @CreatedAt, @UpdatedAt)
            RETURNING id;";
        public static string updateQuote =
            @"UPDATE quotes SET quote=@Quote, updatedAt=@UpdatedAt WHERE id=@Id;";

        public static string deleteQuote =
            @"DELETE FROM quotes WHERE id=@id;";
    }
}