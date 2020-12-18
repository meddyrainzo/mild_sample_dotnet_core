using Models.Exceptions;

namespace Services
{
    public static class Extension
    {
        public static void CheckNull<T>(this T value) where T : class
        {
            if (value is null)
                throw new QuoterException(ErrorReason.EMPTY_DATA_SENT);
        }

        public static void CheckInvalid(this int id)
        {
            if (id < 1)
                throw new QuoterException(ErrorReason.INVALID_ID);
        }
    }
}