using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.App_Data.Controllers.Autenticacion
{
    public class AutenticacionController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AutenticacionController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public AutenticacionController()
        {
        }

        //// GET: Autenticacion
        public ActionResult Index()
        {
            return View();
        }

        public ResponseModel<string> IniciarSesion(string correo, string password)
        {
            try
            {
                // Verificar si el usuario existe en la base de datos
                bool usuarioExiste = VerificarUsuarioEnBaseDeDatos(correo, password);

                if (usuarioExiste)
                {
                    // Obtener nombre del usuario
                    string nombreUsuario = ObtenerNombreUsuario(correo);
                    // Generar un token para la validación de inicio de sesión
                    //string token = GenerarToken(nombreUsuario);

                    // Guardar el token en la base de datos junto con el usuario
                    //GuardarTokenEnBaseDeDatos(correo, token);
                    //GuardarTokenEnCookies(token, ControllerContext);
                    var expireMinutes = 20;
                    var (token, expiration) = GenerateToken(nombreUsuario, expireMinutes);
                    //GenerateToken(nombreUsuario);
                    // Verificar el token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Convert.ToBase64String(new HMACSHA256().Key))),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                    SecurityToken validatedToken;
                    var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                    // Obtener el token y el tiempo de expiración
                    var tokenString = tokenHandler.WriteToken(validatedToken);
                    var expirationTime = validatedToken.ValidTo;

                    Console.WriteLine($"Token: {tokenString}");
                    Console.WriteLine($"Tiempo de expiración: {expirationTime}");

                    return new ResponseModel<string>
                    {
                        StatusCode = 200,
                        Message = "Acceso",
                        Data = token,
                    };
                }
                else
                {
                    return new ResponseModel<string>
                    {
                        StatusCode = 404,
                        Message = "Usuario no encontrado"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel<string>
                {
                    StatusCode = 500,
                    Message = "Error: " + ex.Message
                };
            }
        }

        private string ObtenerNombreUsuario(string correo)
        {

            // Ejemplo de implementación utilizando Entity Framework:
            using (var dbContext = new DB_WSBEntities())
            {
                var usuario = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == correo);
                return usuario != null ? usuario.sNombre : string.Empty;
            }
        }


        private bool VerificarUsuarioEnBaseDeDatos(string correo, string password)
        {
            // implementación utilizando Entity Framework:
            using (var dbContext = new DB_WSBEntities())
            {
                var usuario = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == correo && u.sPassword == password);
                return usuario != null;
            }
        }

        private string GenerarToken(string nombreUsuario)
        {
            var dbContext = new DB_WSBEntities();
            // Ejemplo de implementación utilizando JWT:
            var tokenHandler = new JwtSecurityTokenHandler();
            /*var key = Encoding.ASCII.GetBytes("2#9$1aBcD3eFgH5abcdefghijklmnop");*/ // Reemplaza "TuClaveSecreta" con una clave segura
            var key = new byte[32]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, nombreUsuario) // Reemplaza "NombreUsuario" con el nombre del usuario
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Establece la fecha de expiración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //private void GuardarTokenEnCookie(string token)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        Expires = DateTime.UtcNow.AddHours(1) // Establece la fecha de expiración de la cookie
        //    };
        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("Token", token, cookieOptions);
        //    //httpContextAccessor.HttpContext.Response.Cookies.Append("Token", token, cookieOptions);
        //}

        //private void GuardarTokenEnCookies(string token, ControllerContext controllerContext)
        //{

        //    if (ControllerContext == null || ControllerContext.HttpContext == null || ControllerContext.HttpContext.Response == null)
        //    {
        //        // Manejar el caso cuando ControllerContext o HttpContext o Response son null
        //        throw new Exception("No se puede acceder a la respuesta HTTP");
        //    }
        //    // Guardar el token en las cookies
        //    var cookie = new HttpCookie("Token", token);
        //    cookie.Expires = DateTime.UtcNow.AddHours(1); // Establece la fecha de expiración de la cookie
        //    controllerContext.HttpContext.Response.Cookies.Add(cookie);
        //}

        //public static string GenerateToken(string username, int expireMinutes = 20)
        //{
        //    var hmac = new HMACSHA256();
        //    var key = Convert.ToBase64String(hmac.Key);

        //    var symmetricKey = Convert.FromBase64String(key);
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    var now = DateTime.UtcNow;
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //    new Claim(ClaimTypes.Name, username)
        //}),

        //        Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var stoken = tokenHandler.CreateToken(tokenDescriptor);
        //    var token = tokenHandler.WriteToken(stoken);

        //    return token;
        //}

        public static (string token, DateTime expiration) GenerateToken(string username, int expireMinutes = 20)
        {
            var hmac = new HMACSHA256();
            var key = Convert.ToBase64String(hmac.Key);

            var symmetricKey = Convert.FromBase64String(key);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username)
        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return (token, tokenDescriptor.Expires.Value);
        }




        private void GuardarTokenEnBaseDeDatos(string correo, string token)
        {
            // Ejemplo de implementación utilizando Entity Framework:
            using (var dbContext = new DB_WSBEntities())
            {
                var usuario = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == correo);
                if (usuario != null)
                {
                    usuario.sToken = token;
                    dbContext.SaveChanges();
                }
            }
        }


    }
}