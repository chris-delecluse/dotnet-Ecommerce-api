using System;
namespace ECommerce.Exceptions;

public class NotFoundException : RequestProblemException
{
    internal NotFoundException(string message) : base(message) { }
}