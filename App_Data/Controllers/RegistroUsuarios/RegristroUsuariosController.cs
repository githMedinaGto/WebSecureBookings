using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class RegristroUsuariosController
    {

        // REGISTRO DE CLIENTES
        [HttpPost]
        public ResponseModel<string> PostCrearCliente(int sRol, string sNombre, string sApellidoP, string sApellidoM, string sCorreo, string sPassword)
        {
            try
            {
                bool usuarioExiste = VerificarUsuarioEnBaseDeDatos(sCorreo, sNombre, sApellidoP, sApellidoM);
                if (!usuarioExiste)
                {
                    using (var dbContext = new DB_WSBEntities())
                    {
                        EncriptionController encriptionController = new EncriptionController();
                        string encriPass = encriptionController.Encrypt(sPassword);

                        var oCliente = new tUsuario();
                        oCliente.sNombre = sNombre;
                        oCliente.sApellidoP = sApellidoP;
                        oCliente.sApellidoM = sApellidoM;
                        oCliente.sCorreo = sCorreo;
                        oCliente.sPassword = encriPass;
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
                else
                {
                    return new ResponseModel<string>
                    {
                        StatusCode = 500,
                        Message = "El usuario ya existe"
                    };
                }

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



        // REGISTRO DE PROFESIONISTAS
        [HttpPost]
        public ResponseModel<string> PostCrearProfesionista(int sRol, string sNombre, string sApellidoP, string sApellidoM, string sCorreop, string sPassword01, string sProfesion, string sTelefono, string sArea, int iMunicipio, string sColonia, string sCalle, string sUbicacion)
        {

            try
            {
                bool usuarioExiste = VerificarUsuarioPEnBaseDeDatos(sCorreop, sNombre, sApellidoP, sApellidoM, sTelefono);
                if (!usuarioExiste)
                {
                    using (var dbContext = new DB_WSBEntities())
                    {
                        EncriptionController encriptionController = new EncriptionController();
                        string encriPass = encriptionController.Encrypt(sPassword01);

                        var oCliente = new tUsuario();
                        oCliente.sNombre = sNombre;
                        oCliente.sApellidoP = sApellidoP;
                        oCliente.sApellidoM = sApellidoM;

                        oCliente.sCorreo = sCorreop;
                        oCliente.sPassword = encriPass;
                        oCliente.idRol = sRol;
                        oCliente.stelefono = sTelefono;

                        oCliente.sProfecion = sProfesion;
                        oCliente.sAreaProfesion = sArea;
                        oCliente.sColonia = sColonia;
                        oCliente.sUbicacion = sUbicacion;
                        oCliente.sCalle = sCalle;
                        oCliente.idMunicipio = iMunicipio;

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
                else
                {
                    return new ResponseModel<string>
                    {
                        StatusCode = 500,
                        Message = "El usuario ya existe"
                    };
                }
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
                        Message = "No se pudo guardar los datos de la base de datos: "
                    };
                }
            }
        }



        private bool VerificarUsuarioEnBaseDeDatos(string correo, string nombre, string apellidoP, string apellidoM)
        {
            // implementación utilizando Entity Framework:
            using (var dbContext = new DB_WSBEntities())
            {
                var usuario = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == correo  || u.sNombre == nombre && u.sApellidoP == apellidoP && u.sApellidoM == apellidoM);
                return usuario != null;
            }
        }

        private bool VerificarUsuarioPEnBaseDeDatos(string correo, string nombre, string apellidoP, string apellidoM, string sTelefono)
        {
            // implementación utilizando Entity Framework:
            using (var dbContext = new DB_WSBEntities())
            {
                var usuario = dbContext.tUsuario.FirstOrDefault(u => u.sCorreo == correo || u.stelefono == sTelefono || u.sNombre == nombre && u.sApellidoP == apellidoP && u.sApellidoM == apellidoM);
                return usuario != null;
            }
        }
    }
}