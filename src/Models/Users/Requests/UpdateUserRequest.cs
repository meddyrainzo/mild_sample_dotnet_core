using System;
using Models.Exceptions;

namespace Models.Users
{
    public class UpdateUserRequest
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        private UpdateUserRequest(int id, string userName)
        {
            Id = id;
            UserName = userName;
        }

        public static UpdateUserRequest CreateRequest(int id, string userName)
        {
            var request = new UpdateUserRequest(id, userName);
            var validator = new UpdateUserValidator();
            var validationResult = validator.Validate(request);
            return validationResult.IsValid ? request : throw new QuoterException(String.Join(", ", validationResult.Errors));
        }
    }

}