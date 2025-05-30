using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tsi.Erp.TestTracker.Api.Security;

public class JwtSecurityTokenService
{
    private JwtBearerSettings _configuration { get; set; }
    public JwtSecurityTokenService(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("JwtConfiguration")
            .Get<JwtBearerSettings>();
    }
    public string WriteToken(Claim claim) =>
        WriteToken(new Claim[] {claim });

    public string WriteToken(Claim[] claims)
    {
        ArgumentNullException.ThrowIfNull(claims, nameof(claims));
        var subject = new ClaimsIdentity(claims);
        return WriteToken(subject);
    }

    public string WriteToken(ClaimsIdentity claimsIdentity)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret));
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = _configuration.ValidIssuer,
            Audience = _configuration.ValidAudience,
            //Expires = DateTime.Now.AddHours(1),
            SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GetClaim(string token, string claimType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(token);
        var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
        return stringClaimValue;
    }
}
