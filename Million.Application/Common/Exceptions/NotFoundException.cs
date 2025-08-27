namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException(string name, object key) : Exception($"'{name}' with key '{key}' was not found.")
    {
    }
}
