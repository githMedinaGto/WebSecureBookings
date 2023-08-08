using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.App_Data.Controllers.Autenticacion
{
    public class AutenticacionController : Controller
    {
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
                    string token = GenerarToken(nombreUsuario);

                    // Guardar el token en la base de datos junto con el usuario
                    GuardarTokenEnBaseDeDatos(correo, token);

                    return new ResponseModel<string>
                    {
                        StatusCode = 200,
                        Message = "Acceso",
                        Data = token
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
                return usuario != null ? usuario.idUsuario.ToString() : string.Empty;
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
            var key = Encoding.ASCII.GetBytes("2#9$1aBcD3eFgH5"); // Reemplaza "TuClaveSecreta" con una clave segura
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
            HttpContext.CurrentNotification.Response.Cookies.Append("Token", token);
            return tokenHandler.WriteToken(token);
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