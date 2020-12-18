namespace Models.Exceptions
{
    public class ErrorReason
    {
        public static readonly string EMPTY_DATA_SENT = "no_data_sent";
        public static readonly string USER_NOT_FOUND = "user_not_found";
        public static readonly string QUOTE_NOT_FOUND = "quote_not_found";
        public static readonly string NOT_PERMITTED = "not_permitted_to_perform_action";
        public static readonly string BOOKMARK_NOT_FOUND = "bookmark_not_found";
        public static readonly string USER_EXISTS = "user_already_exists";
        public static readonly string EMPTY_USERNAME = "username_cannot_be_empty";
        public static readonly string INVALID_ID = "zero_is_not_a_valid_id";

        public static readonly string EMPTY_QUOTE = "quote_cannot_be_empty";
        public static readonly string EMPTY_COMMENT = "comment_cannot_be_empty";
    }
}