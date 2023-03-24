using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Config;

public static class _JwtOptions
{
    public static Action<JwtBearerOptions> ForTokenValidation(IConfiguration configuration) => options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.GetValue<string>("Jwt:Issuer"),
            ValidateAudience = true,
            ValidAudience = configuration.GetValue<string>("Jwt:Audience"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetValue<string>("Jwt:Key")!)
            ),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    };
}