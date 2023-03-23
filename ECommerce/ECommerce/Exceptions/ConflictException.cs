using System;
namespace ECommerce.Exceptions;

public class ConflictException : RequestProblemException
{
    internal ConflictException(string message) : base(message) { }
}