namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Forbidden Access Exception
    /// </summary>
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException(string message = "Access denied.") : base(message) { }
    }
}
