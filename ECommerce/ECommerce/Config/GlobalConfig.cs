using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerce.Config;

public static class GlobalConfig
{
    private static IConfiguration? _configuration;

    public static void InitializeConfig(IConfiguration configuration) => _configuration = configuration;

    public static Action<JwtBearerOptions> GetTokenValidationOptions() => _JwtOptions.ForTokenValidation(_configuration!);
    public static Action<JsonOptions> GetJsonOptions() => _JsonOptions.ForJsonOptions();
    public static Action<SwaggerGenOptions> GetSwaggerGenOptions() => _SwaggerOptions.ForSwaggerOptions();
    public static Action<CookieAuthenticationOptions> GetCookieAuthenticationOptions() => _CookieOptions.ForCookieAuthenticationOptions();
}