using System;
using ECommerce.Models;

namespace ECommerce.Exceptions;

public class RequestProblemException : Exception
{
    protected RequestProblemException(string message) : base(message) { }

    public static RequestProblemException ForMissingField(string missingFieldName) =>
        new BadRequestException($"The value for field '{missingFieldName.ToLower()}' is missing.");

    public static RequestProblemException ForInvalidField(string invalidFieldName) =>
        new BadRequestException($"The value for field '{invalidFieldName}' is invalid");

    public static RequestProblemException ForConflictingItem<T>(T item) where T : IBaseModel
        => new ConflictException($"The item with id '{item.Id}' already exist");

    public static RequestProblemException ForNotFound(string itemSpecificField) =>
        new NotFoundException($"The item with id '{itemSpecificField} was not found");

    public static RequestProblemException ForInvalidCredentials() =>
        new InvalidCredentialException("Invalid Credentials");
}