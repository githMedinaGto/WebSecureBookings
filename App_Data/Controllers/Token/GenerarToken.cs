using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebSecureBookings
{
    public class Generar_Token
    {
            private const string SecretKey = "MiClaveSecreta12345678901234567890"; // Clave de 32 bytes (256 bits)

            public static string GenerateToken(string userId, string role)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role)
                }),
                    Expires = DateTime.UtcNow.AddSeconds(6000),// Establece el tiempo de expiración a 2 minutos
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                SaveTokenToCache(tokenString);
                return tokenString;
            }

            private static void SaveTokenToCache(string token)
            {
                HttpContext.Current.Session["token"] = token;
            }

            public static string GetTokenFromCache()
            {
                return (string)HttpContext.Current.Session["token"];
            }

            public static Dictionary<string, string> DecodeToken(string token)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                try
                {
                    var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                    var claims = new Dictionary<string, string>();
                    foreach (var claim in claimsPrincipal.Claims)
                    {
                        claims.Add(claim.Type, claim.Value);
                    }
                    return claims;
                }
                catch
                {
                    Console.WriteLine("Error al decodificar el token");
                    return null;
                }
            }

            public static bool IsTokenExpired(string token)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(SecretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                try
                {
                    var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
                    return false; // El token no ha expirado
                }
                catch (SecurityTokenExpiredException)
                {
                    return true; // El token ha expirado
                }
                catch
                {
                    return true; // Otro error al validar el token
                }
            }

        // Verificar si el token se ha creado y no ha expirado
        public static string RedirecAPaginaPrincipal(string obtenerToken)
        {
            if (!Generar_Token.IsTokenExpired(obtenerToken))
            {
                string url = "/Views/PerfilesProfecionista/PerfilesProfesionistas.aspx";
                return url;
                // Redirigir a la página principal
                //HttpContext.Current.Response.Redirect(url); // Usar HttpContext.Current.Response // Redirigir a la página principal
            }
            else
            {
                //HttpContext.Current.Response.Redirect(
                return "/Views/RegistroUsuarios/RegistroUsuarios.aspx";//);
            }
        }
    }
}