using System;
using Models.Exceptions;

namespace Models.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        private CreateUserRequest(string username) => UserName = username;

        public static CreateUserRequest CreateRequest(string username)
        {
            var request = new CreateUserRequest(username.Trim());
            var validator = new CreateUserValidator();
            var result = validator.Validate(request);
            return result.IsValid ? request : throw new QuoterException(String.Join(", ", result.Errors));

        }

    }

}