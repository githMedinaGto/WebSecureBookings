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
        public AutenticacionController()
        {
        }

        public ResponseModel<string> IniciarSesion(string correo, string password)
        {
            try
            {
                EncriptionController encriptionController = new EncriptionController();
                string encriPass = encriptionController.Encrypt(password);
                // Verificar si el usuario existe en la base de datos
                bool usuarioExiste = VerificarUsuarioEnBaseDeDatos(correo, encriPass);

                if (usuarioExiste)
                {
                    // Obtener nombre del usuario
                    List <UsuarioModel> lstUsuario = ObtenerNombreUsuario(correo, encriPass);

                    List<string> idUsuariosStrings = new List<string>();
                    List<string> idRolesStrings = new List<string>();

                    foreach (var usuario in lstUsuario)
                    {
                        idUsuariosStrings.Add(usuario.idUsuario.ToString());
                        idRolesStrings.Add(usuario.idRol.ToString());
                    }

                    string idUsuariosString = string.Join(", ", idUsuariosStrings);
                    string idRolesString = string.Join(", ", idRolesStrings);
                    // Generar un token para la validación de inicio de sesión

                    var token = Generar_Token.GenerateToken(idUsuariosString, idRolesString);

                    var optenertoken = Generar_Token.GetTokenFromCache();


                    string url = Generar_Token.RedirecAPaginaPrincipal(token);

                    return new ResponseModel<string>
                    {
                        StatusCode = 200,
                        Message = "Inicio sesion correctamente",
                        Data = url,
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

        private List<UsuarioModel> ObtenerNombreUsuario(string correo, string sPassword)
        {
            using (var dbContext = new DB_WSBEntities())
            {
                var usuarios = (from d in dbContext.tUsuario
                                where d.sCorreo == correo & d.sPassword == sPassword
                                select new UsuarioModel
                                {
                                    idRol = (int)d.idRol,
                                    idUsuario = d.idUsuario,
                                }).ToList();

                return usuarios;
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


        [HttpGet]
        public ResponseModel<List<MunicipioModel>> ObtenerMunicipios()
        {

            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Realizar una consulta para obtener una lista de estados y mapearlos a objetos de modelo de estado
                    var lst = (from d in dbContext.tMunicipio
                               select new MunicipioModel
                               {
                                   idMunicipio = d.idMunicipio,
                                   sMunicipio = d.sMunicipio,   
                               }).ToList();

                    return new ResponseModel<List<MunicipioModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = lst
                    };
                }
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<MunicipioModel>>>(ex);

                return new ResponseModel<List<MunicipioModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }
    }
}