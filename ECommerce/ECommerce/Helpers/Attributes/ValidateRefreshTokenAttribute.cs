using System.Text.Json;
using ECommerce.Exceptions;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateRefreshToken : TypeFilterAttribute
{
    public ValidateRefreshToken() : base(typeof(ValidateRefreshTokenAttribute))
    {
    }

    private class ValidateRefreshTokenAttribute : IAsyncResultFilter
    {
        private readonly ITokenService _tokenService;

        public ValidateRefreshTokenAttribute(ITokenService tokenService) =>
            _tokenService = tokenService;

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            try
            {
                string refreshTokenCookie = _tokenService.GetCookieRefreshToken(context.HttpContext.Request);

                if (refreshTokenCookie is null)
                    throw RequestProblemException.ForInvalidCredentials();

                RefreshToken? token = await _tokenService.GetOneByRefreshToken(refreshTokenCookie!);

                if (token is null)
                    throw RequestProblemException.ForInvalidCredentials();

                if (token!.ExpireIn < DateTime.Now)
                    throw RequestProblemException.ForInvalidCredentials();

                // attacher le token à la requete pour que je puisse
                // le récupere dans le controller pour éviter de refaire une requete supplémentaire
                
                await next();
            }
            catch (InvalidCredentialException e)
            {
                context.HttpContext.Response.StatusCode = 401;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new { e.Message }));
            }
        }
    }
}