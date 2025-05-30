using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tsi.Erp.TestTracker.Api.Extentions;

namespace Tsi.Erp.TestTracker.Api.Security;

public static class JwtBearerAuthenticationExtentions
{

    public static AuthenticationBuilder AddJwtBearerAuthentication(this AuthenticationBuilder builder, JwtBearerSettings configuration)
    {
        builder.AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration.ValidAudience,
                ValidIssuer = configuration.ValidIssuer,
                RequireExpirationTime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Secret))
            };
        });

        return builder;
    }

    public static AuthenticationBuilder AddJwtBearer(this AuthenticationBuilder builder, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection<JwtBearerSettings>("JwtConfiguration");
        builder.AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.ValidAudience,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    RequireExpirationTime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),                    
                };
            });

        return builder;
    }
}
