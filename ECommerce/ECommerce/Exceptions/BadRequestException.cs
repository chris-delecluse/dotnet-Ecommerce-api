using System;
namespace ECommerce.Exceptions;

public class BadRequestException : RequestProblemException
{
    internal BadRequestException(string message) : base(message) { }
}