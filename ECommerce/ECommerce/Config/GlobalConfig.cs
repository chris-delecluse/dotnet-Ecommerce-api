using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Config;

public static class GlobalConfig
{
    public static Action<JwtBearerOptions> GetTokenValidationOptions() =>
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:5500",
                ValidateAudience = true,
                ValidAudience = "http://localhost:5500",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("mySuperSecretKey")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        };

    public static Action<JsonOptions> GetJsonOptions() =>
        options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        };
}