using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Minimalist.Api.DTOs;

namespace Todo.Minimalist.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/login", (LoginDto login, IConfiguration config) =>
        {
            if (login.Username != "admin" || login.Password != "123")
            {
                return Results.Unauthorized();
            }

            var jwtSection = config.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

            var expires = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(new
            {
                access_token = tokenString,
                token_type = "Bearer",
                expires_in = (int)(expires - DateTime.UtcNow).TotalSeconds,
                scope = "read write"
            });
        });

        return app;
    }
}
