using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class PerfilesUsaurioController
    {
        #region Metodo que optiene los perfiles de los profesionistas
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistas()
        {

            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                    // Decodificar el token para obtener los claims
                    Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                    string userId = claims[ClaimTypes.NameIdentifier];
                    int iIdUser = int.Parse(userId);

                    // Realizar una consulta para obtener el profesionista con el id proporcionado
                    var lst = (from d in dbContext.tUsuario
                               join m in dbContext.tMunicipio on d.idMunicipio equals m.idMunicipio into municipioGroup
                               from m in municipioGroup.DefaultIfEmpty()
                               join e in dbContext.tEstado on m.idMunicipio equals e.idMunicipio into estadoGroup
                               from e in estadoGroup.DefaultIfEmpty()
                                   // Aplicar filtros para obtener solo el profesionista con el id y que esté activo
                               where (d.idUsuario == iIdUser)
                               select new
                               {
                                   Usuario = d,
                                   Municipio = m != null ? m.sMunicipio : null,
                                   Estado = e != null ? e.sEstado : null
                               }).ToList();

                    // Mapear los resultados obtenidos a objetos UsuarioModel
                    var usuarios = lst.Select(item => new UsuarioModel
                    {
                        bEstatus = (bool)item.Usuario.bEstatus,
                        idUsuario = item.Usuario.idUsuario,
                        sNombre = item.Usuario.sNombre,
                        sApellidoP = item.Usuario.sApellidoP,
                        sApellidoM = item.Usuario.sApellidoM,
                        sProfecion = item.Usuario.sProfecion,
                        sAreaProfesion = item.Usuario.sAreaProfesion,
                        stelefono = item.Usuario.stelefono,
                        sCorreo = item.Usuario.sCorreo,
                        idMunicipio = (int)item.Usuario.idMunicipio,
                        sColonia = item.Usuario.sColonia,
                        sCalle = item.Usuario.sCalle,
                        sMunicipio = item.Municipio,
                        sEstado = item.Estado,
                        sUbicacion = item.Usuario.sUbicacion

                    }).ToList();

                    // Devolver una respuesta exitosa con los datos obtenidos
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = usuarios
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
        #endregion


        #region Metodo que actualiza la ubicacion
        [HttpPost]
        public ResponseModel<string> PutUbicacon(string sUbicacion)
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                                                                   // Decodificar el token para obtener los claims
                    Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                    string userId = claims[ClaimTypes.NameIdentifier];
                    int iIdUser = int.Parse(userId);

                    var upUser = dbContext.tUsuario.Find(iIdUser);
                    upUser.sUbicacion = sUbicacion; 
                    dbContext.Entry(upUser).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                // Devolver una respuesta exitosa
                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos actuallizados exitosamente"
                };
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<string>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }
        #endregion

        #region Metodo que actualiza la ubicacion
        [HttpPost]
        public ResponseModel<string> PutUsuario(string sProfecion, string sAreaProfesion, string sTelefono, string sCorreo,
                string idMunicipio, string sColonia, string sCalle, string sBEstatus)
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                                                                   // Decodificar el token para obtener los claims
                    Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                    string userId = claims[ClaimTypes.NameIdentifier];
                    int iIdUser = int.Parse(userId);

                    var upUser = dbContext.tUsuario.Find(iIdUser);
                    upUser.sProfecion = sProfecion;
                    upUser.sAreaProfesion = sAreaProfesion;
                    upUser.stelefono = sTelefono;
                    upUser.sCorreo = sCorreo;
                    upUser.idMunicipio = int.Parse(idMunicipio);
                    upUser.sColonia = sColonia;
                    upUser.sCalle = sCalle;
                    upUser.bEstatus = bool.Parse(sBEstatus);
                    dbContext.Entry(upUser).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                // Devolver una respuesta exitosa
                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos actuallizados exitosamente"
                };
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<string>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }
        #endregion
    }
}
