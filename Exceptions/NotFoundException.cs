namespace ProvaPub.Exceptions
{
    public class RelatedAggregateException : ApplicationException
    {
        public RelatedAggregateException(string? message) : base(message)
        { }
    }
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string? message) : base(message)
        {
        }

        public static void ThrowIfNull(
            object? @object,
            string exceptionMessage)
        {
            if (@object is null)
                throw new NotFoundException(exceptionMessage);
        }
    }
}
