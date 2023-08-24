using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class RecuperarCuentaController
    {
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> BuscarCuenta(string sCorreo)
        {
            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Realizar una consulta para obtener una lista de usuarios con información adicional
                    var lst = (from d in dbContext.tUsuario
                               where d.sCorreo == sCorreo// Agregar la condición bEstatus = 1
                               select new
                               {
                                   Usuario = d
                               }).ToList();

                    if (lst.Count == 0) // Verificar si la lista está vacía
                    {
                        return new ResponseModel<List<UsuarioModel>>
                        {
                            StatusCode = 404, // Código para "No encontrado"
                            Message = "Usuario no encontrado"
                        };
                    }

                    // Mapear los resultados de la consulta a objetos de modelo de usuario
                    var usuarios = lst.Select(item => new UsuarioModel
                    {
                        idUsuario = item.Usuario.idUsuario,
                        sNombre = item.Usuario.sNombre + ' ' + item.Usuario.sApellidoP + ' ' + item.Usuario.sApellidoM
                    }).ToList();

                    int idUsuario = lst.FirstOrDefault()?.Usuario.idUsuario ?? -1;
                    string susuario = lst.FirstOrDefault()?.Usuario?.sNombre;
                    string token = GenerateToken();
                    string sGuardartoeken = PutUsuario(token, idUsuario);

                    if(sGuardartoeken != "1")
                    {
                        return new ResponseModel<List<UsuarioModel>>
                        {
                            StatusCode = 404,
                            Message = "Error al generar el token",
                            Data = usuarios
                        };
                    }

                    _ = Task.Run(async () =>
                    {
                        await EnviarToken(sCorreo, susuario, token);
                    });

                    // Devolver una respuesta exitosa con los datos obtenidos
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 200,
                        Message = "Se le enviara un correo con un token para actualizar su contraseña",
                        Data = usuarios
                    };

                    //bool envioTokenExitoso = EnviarToken(sCorreo, susuario, token).Result;
                }
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<List<UsuarioModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }

        #region Metodo que actualiza el token
        [HttpPost]
        public string PutUsuario(string sToken, int iIdusuario)
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {

                    var upUser = dbContext.tUsuario.Find(iIdusuario);
                    upUser.sToken = sToken;
                    dbContext.Entry(upUser).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                // Devolver una respuesta exitosa
                return "1";
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return "2";
            }
        }
        #endregion


        public static string GenerateToken()
        {
             Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder token = new StringBuilder(5);

            for (int i = 0; i < 5; i++)
            {
                token.Append(chars[random.Next(chars.Length)]);
            }

            return token.ToString();
        }

        public async Task<bool> EnviarToken(string toAddress, string userName, string sToken)
        {
            // Crear una instancia de EnvioDeCorreoController
            EnvioDeCorreoController correoController = new EnvioDeCorreoController();

            bool envioExitoso = await correoController.EnviarCorreoBienvenidaAsync(toAddress, userName, sToken);

            return envioExitoso;
        }

        [HttpGet]
        public ResponseModel<List<UsuarioModel>> ActualizarContrasenia(string sCorreo, string sContrasenia, string sToken)
        {
            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Buscar un usuario con el correo y el token proporcionados
                    var upUser = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == sCorreo && u.sToken == sToken);

                    if (upUser == null)
                    {
                        // No se encontró el usuario con el correo y token proporcionados
                        return new ResponseModel<List<UsuarioModel>>
                        {
                            StatusCode = 404,
                            Message = "Correo o token incorrecto"
                        };
                    }

                    EncriptionController encriptionController = new EncriptionController();
                    string encriPass = encriptionController.Encrypt(sContrasenia);
                    // Actualizar los datos del usuario
                    upUser.sToken = null;
                    upUser.sPassword = encriPass;
                    dbContext.Entry(upUser).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                    // Devolver una respuesta exitosa con el mensaje
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 200,
                        Message = "Se ha actualizado correctamente la contraseña"
                    };
                }
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<List<UsuarioModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }

        }
    }
}