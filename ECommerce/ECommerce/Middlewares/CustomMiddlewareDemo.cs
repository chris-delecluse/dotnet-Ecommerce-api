// using System.Security.Claims;
// using ECommerce.Services;
//
// namespace ECommerce.Middlewares;
//
// public class CustomMiddlewareDemo
// {
//     private readonly RequestDelegate _next;
//
//     public CustomMiddlewareDemo(RequestDelegate next)
//     {
//         _next = next;
//     }
//
//     public async Task Invoke(HttpContext httpContext)
//     {
//         if (!httpContext.Response.HasStarted)
//         {
//             httpContext.RequestServices.GetService<// SERVICE EXAMPLE>()
//             Console.WriteLine("hello from custom middleware DEM0");
//         }
//
//         await _next(httpContext);
//     }
// }