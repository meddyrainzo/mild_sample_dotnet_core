namespace Repository.Bookmarks
{
    public static class BookmarkSqlStatements
    {
        public static readonly string getBookmarks =
            @"SELECT b.userId, b.quoteId, q.id, q.userId, q.quote, q.createdAt, q.updatedAt,
             u.username, 
             (SELECT COUNT(*) FROM likes WHERE quoteId=b.quoteId) as numberOfLikes, 
			 (SELECT COUNT(*) id FROM comments WHERE quoteId=b.quoteId) as numberOfComments,
             (SELECT COUNT(quoteId) FROM bookmarks WHERE quoteId=b.quoteId) as numberOfBookmarks,
             (SELECT COUNT(1) FROM likes WHERE quoteId=q.id AND userId=@userId) AS likedByYou,
             (SELECT 1) as bookmarkedByYou
             FROM bookmarks as b INNER JOIN quotes as q ON q.id=b.quoteId
             INNER JOIN users as u ON u.id=q.userId
             WHERE b.userId=@userId OFFSET @skip LIMIT @limit;";


        public static readonly string createdBookmark =
            @"INSERT INTO bookmarks(userId, quoteId, createdAt) 
            VALUES(@UserId, @QuoteId, @CreatedAt)
            RETURNING id;";

        public static readonly string removeBookmark =
            @"DELETE FROM bookmarks WHERE id=@id;";

        public static readonly string getBookmarkById =
            @"SELECT * FROM bookmarks WHERE id=@id;";
    }
}