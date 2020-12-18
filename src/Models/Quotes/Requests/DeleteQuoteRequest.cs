using Models.Exceptions;

namespace Models.Quotes
{
    public class DeleteQuoteRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        private DeleteQuoteRequest(int id, int userid)
        {
            Id = id;
            UserId = userid;
        }

        public static DeleteQuoteRequest CreateRequest(int id, int userid)
        {
            var request = new DeleteQuoteRequest(id, userid);
            var validator = new DeleteQuoteValidator();
            var result = validator.Validate(request);

            return result.IsValid ? request : throw new QuoterException(string.Join(", ", result.Errors));
        }

    }

}