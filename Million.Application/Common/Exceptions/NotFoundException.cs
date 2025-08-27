namespace Million.Application.Common.Exceptions
{
    /// <summary>
    /// Not found exception
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"'{name}' with key '{key}' was not found.") { }
    }
}
