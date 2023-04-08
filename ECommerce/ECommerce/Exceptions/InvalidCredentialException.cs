namespace ECommerce.Exceptions;

public class InvalidCredentialException : RequestProblemException
{
    internal InvalidCredentialException(string message) : base(message) { }
}