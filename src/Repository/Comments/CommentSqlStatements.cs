namespace Repository.Comments
{
    public static class CommentSqlStatements
    {
        public static readonly string getComments =
            @"SELECT c.id, c.quoteId, c.userId, c.comment, c.createdAt, u.username AS commenter
             FROM comments as c INNER JOIN users AS u ON u.id=c.userId WHERE c.quoteId=@quoteId;";
        public static readonly string createComment =
            @"INSERT INTO comments(userId, quoteId, comment, createdAt) 
            VALUES(@UserId, @QuoteId, @Comment, @CreatedAt)
            RETURNING id;";
    }
}