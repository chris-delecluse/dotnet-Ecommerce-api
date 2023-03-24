using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Helpers;

public class JwtTokenBuilder
{
    private readonly SymmetricSecurityKey _securityKey;
    private string? _issuer;
    private string? _audience;
    private List<Claim> _claims = new();
    private DateTime _expires = DateTime.Now.AddMinutes(30);

    public JwtTokenBuilder(string secret)
    {
        _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }

    public JwtTokenBuilder AddIssuer(string issuer)
    {
        _issuer = issuer;
        
        return this;
    }

    public JwtTokenBuilder AddAudience(string audience)
    {
        _audience = audience;
        
        return this;
    }

    public JwtTokenBuilder AddClaim(string type, string value)
    {
        _claims.Add(new Claim(type, value));
        
        return this;
    }

    public JwtTokenBuilder SetExpiration(DateTime expiration)
    {
        _expires = expiration;
        
        return this;
    }

    public string Build()
    {
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Issuer = _issuer,
            Audience = _audience,
            Expires = _expires,
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(_claims)
        };
        
        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}