namespace Million.Application.Common.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("User or password invalid") { }
    }
}
