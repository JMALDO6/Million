namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Validation Exception
    /// </summary>
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors)
            : base("Validation failed.")
        {
            Errors = errors;
        }
    }
}
