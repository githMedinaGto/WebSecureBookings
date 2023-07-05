using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.Controllers.IndexController
{
    public class IndexController
    {
        [HttpGet]
        public ResponseModel<List<RolModel>> GetRolDataTable()
        {
            
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var lst = (from d in dbContext.tRol
                               select new RolModel
                               {
                                   idRol = d.idRol,
                                   sNomRol = d.sNomRol,
                                   sDescripcion = d.sDescripcion
                               }).ToList();

                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = lst
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpPost]
        public ResponseModel<string> PostCrearRol(string sRol, string sDescripcion)
        {
            
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var oRol = new tRol();
                    oRol.sNomRol = sRol;
                    oRol.sDescripcion = sDescripcion;

                    dbContext.tRol.Add(oRol);
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

        [HttpGet]
        public ResponseModel<List<RolModel>> GetRolId(string idRol) {
            try
            {
                RolModel model= new RolModel();
                using (var dbContext = new DB_WSBEntities())
                {
                    int iIdROl = int.Parse(idRol); // Convertir idRol a int
                    var oRol = dbContext.tRol.Find(iIdROl);
                    model.idRol = oRol.idRol;
                    model.sNomRol = oRol.sNomRol;
                    model.sDescripcion = oRol.sDescripcion;

                    // Crear una lista que contenga la instancia del modelo
                    List<RolModel> lst = new List<RolModel>();
                    lst.Add(model);

                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = lst
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpPut]
        public ResponseModel<string> PutRol(string idRol,string sRol, string sDescripcion)
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    int iIdROl = int.Parse(idRol); // Convertir idRol a int
                    var oRol = dbContext.tRol.Find(iIdROl);

                    //var oRol = new tRol();
                    oRol.sNomRol = sRol;
                    oRol.sDescripcion = sDescripcion;
                    oRol.idRol = iIdROl;

                    dbContext.Entry(oRol).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }

                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos actualizados exitosamente"
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

        [HttpDelete]
        public ResponseModel<List<RolModel>> DeleteRolId(string idRol)
        {
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    int iIdROl = int.Parse(idRol); // Convertir idRol a int
                    var oRol = dbContext.tRol.Find(iIdROl);
                    dbContext.tRol.Remove(oRol);
                    dbContext.SaveChanges();

                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos eliminados correctamente",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<RolModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }
    }

}