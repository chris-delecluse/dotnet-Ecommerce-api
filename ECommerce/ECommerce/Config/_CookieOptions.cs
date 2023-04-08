using Microsoft.AspNetCore.Authentication.Cookies;

namespace ECommerce.Config;

public class _CookieOptions
{
    public static Action<CookieAuthenticationOptions> ForCookieAuthenticationOptions() => options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    };
}