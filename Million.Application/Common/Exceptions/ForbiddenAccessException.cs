namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Forbidden Access Exception
    /// </summary>
    public class ForbiddenAccessException(string message = "Access denied.") : Exception(message)
    {
    }
}
