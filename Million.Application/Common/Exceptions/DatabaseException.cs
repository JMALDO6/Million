namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Database Exception
    /// </summary>
    public class DatabaseException(string message, Exception innerException) : Exception(message, innerException)
    {
    }
}
