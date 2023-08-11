using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace WebSecureBookings
{
    public class GenerarToken
    {
        public string GenerarTokenUsuario()
        {
            var secretKey = "tu_clave_secreta"; // Clave secreta para firmar el token
            var issuer = "tu_issuer"; // Emisor del token
            var audience = "tu_audience"; // Audiencia del token

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "nombreUsuario"),
                new Claim(ClaimTypes.Role, "rolUsuario"),
                // Agrega más claims según tus necesidades
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), // Duración del token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void ObtenerDatosDelToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "tu_clave_secreta"; // Clave secreta para validar la firma del token

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "tu_issuer",
                ValidAudience = "tu_audience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Obtener los datos del usuario del token
                var userId = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                var role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value;

                // Realizar las operaciones necesarias con los datos del usuario
                // ...
            }
            catch (SecurityTokenException)
            {
                // El token no es válido
            }
        }
    }
}