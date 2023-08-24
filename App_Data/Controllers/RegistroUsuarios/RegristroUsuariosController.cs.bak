using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class RegristroUsuariosController
    {
        [HttpPost]
        public ResponseModel<string> PostCrearCliente(int sRol, string sNombre, string sApellidoP, string sApellidoM, string sCorreo, string sPassword)
        {
            //string sNombre, string sApellidoP, string sApellidoM, string sCorreo, string sPassword, string sProfesion, string sTelefono, string sAreaProfesion, string sColonia, string sUbicacion
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {

                        var oCliente = new tUsuario();
                        oCliente.sNombre = sNombre;
                        oCliente.sApellidoP = sApellidoP;
                        oCliente.sApellidoM = sApellidoM;
                        oCliente.sCorreo = sCorreo;
                        oCliente.sPassword = sPassword;
                        oCliente.idRol = sRol;
                        oCliente.stelefono = "N/A";
                        oCliente.sProfecion = "N/A ";
                        oCliente.sAreaProfesion = "N/A";
                        oCliente.dFechaRegistro = DateTime.Now;


                        dbContext.tUsuario.Add(oCliente);
                        dbContext.SaveChanges();
                    

                }

                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos guardados exitosamente"
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<string>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<string>
                    {
                        StatusCode = 404,
                        Message = "No se pudo guardar los datos de la base de datos: " + ex.Message
                    };
                }
            }
        }


        // REGISTRO DE CLIENTES
        [HttpPost]
        public ResponseModel<string> PostCrearProfesionista(int sRol,string sNombre, string sApellidoP, string sApellidoM, string sCorreop, string sPassword01, string sProfesion, string sTelefono, string sArea, int sMunicipio, string sColonia, string sCalle, string sUbicacion)
        {
            //
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {

                    var oCliente = new tUsuario();
                    oCliente.sNombre = sNombre;
                    oCliente.sApellidoP = sApellidoP;
                    oCliente.sApellidoM = sApellidoM;

                    oCliente.sCorreo = sCorreop;
                    oCliente.sPassword = sPassword01;
                    oCliente.idRol = sRol;
                    oCliente.stelefono = sTelefono; 

                    oCliente.sProfecion = sProfesion;
                    oCliente.sAreaProfesion = sArea;
                    oCliente.sColonia = sColonia;
                    oCliente.sUbicacion = sUbicacion;
                    oCliente.sCalle = sCalle;
                    oCliente.idMunicipio = sMunicipio;

                    oCliente.bEstatus = true;
                    oCliente.dFechaRegistro = DateTime.Now;




                    dbContext.tUsuario.Add(oCliente);
                    dbContext.SaveChanges();


                }

                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos guardados exitosamente"
                };
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<string>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<string>
                    {
                        StatusCode = 404,
                        Message = "No se pudo guardar los datos de la base de datos: " + ex.Message
                    };
                }
            }
        }
    }
}