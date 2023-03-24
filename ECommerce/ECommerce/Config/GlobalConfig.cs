using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ECommerce.Config;

public static class GlobalConfig
{
    public static Action<JwtBearerOptions> GetTokenValidationOptions() => _JwtOptions.ForTokenValidation();

    public static Action<JsonOptions> GetJsonOptions() => _JsonOptions.ForJsonOptions();

    public static Action<SwaggerGenOptions> GetSwaggerGenOptions() => _SwaggerOptions.ForSwaggerOptions();
}