using System.Security.Claims;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.Helpers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateRefreshToken: TypeFilterAttribute
{
    public ValidateRefreshToken() : base(typeof(ValidateRefreshTokenAttribute))
    {
    }
    
    private class ValidateRefreshTokenAttribute: IResultFilter
    {
        private readonly IUserService _userService;

        public ValidateRefreshTokenAttribute(IUserService userService) => _userService = userService;
        
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("executing");
            Console.WriteLine(context.HttpContext.User.FindFirstValue(ClaimTypes.Email));

            string a = context.HttpContext.User.FindFirstValue(ClaimTypes.Email)!;

            Task<User?> user = _userService.GetOneByEmail(a);

            Console.WriteLine(user.Result!.Firstname);
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("executed");
        }
    }
}