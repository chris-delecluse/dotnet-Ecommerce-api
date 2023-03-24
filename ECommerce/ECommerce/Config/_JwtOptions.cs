using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Config;

public static class _JwtOptions
{
    public static Action<JwtBearerOptions> ForTokenValidation() => options =>
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
}