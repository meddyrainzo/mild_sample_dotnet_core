namespace Repository.Users
{
    public static class UserSqlStatements
    {
        public static string createUser =
            @"INSERT INTO users (userName, createdAt, updatedAt)
            VALUES (@UserName, @CreatedAt, @UpdatedAt)
            RETURNING id;";
        public static string getUserById =
            @"SELECT * from users WHERE id=@id LIMIT 1;";

        public static string getuserByName =
            @"SELECT * from users WHERE userName=@Username LIMIT 1";
        public static string updateUser =
            @"UPDATE users
            SET userName=@UserName, updatedAt=@UpdatedAt WHERE id=@Id;";
    }
}