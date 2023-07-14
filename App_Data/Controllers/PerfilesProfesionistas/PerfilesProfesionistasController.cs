using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class PerfilesProfesionistasController
    {
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistas()
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var lst = (from d in dbContext.tUsuario
                               join m in dbContext.tMunicipio on d.idMunicipio equals m.idMunicipio into municipioGroup
                               from m in municipioGroup.DefaultIfEmpty()
                               join e in dbContext.tEstado on m.idMunicipio equals e.idMunicipio into estadoGroup
                               from e in estadoGroup.DefaultIfEmpty()
                               where d.bEstatus == true  && d.idRol ==1// Agregar la condición bEstatus = 1
                               select new
                               {
                                   Usuario = d,
                                   Municipio = m != null ? m.sMunicipio : null,
                                   Estado = e != null ? e.sEstado : null
                               }).ToList();

                    var usuarios = lst.Select(item => new UsuarioModel
                    {
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
                        sEstado = item.Estado
                    }).ToList();

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
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<string>> GetProfesiones()
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var profesiones = (from d in dbContext.tUsuario
                                       where d.bEstatus == true && d.idRol == 1
                                       select d.sProfecion).Distinct().ToList();

                    return new ResponseModel<List<string>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = profesiones
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<string>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<string>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<EstadoModel>> GetCiudades()
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var lst = (from d in dbContext.tEstado
                               select new EstadoModel
                               {
                                   idEstado = d.idEstado,
                                   sEstado = d.sEstado,
                                   idMunicipio = (int)d.idMunicipio
                               }).ToList();

                    return new ResponseModel<List<EstadoModel>>
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
                    return new ResponseModel<List<EstadoModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<EstadoModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasEstado( int iEstado)
        {
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    if (iEstado != 0)
                    {
                        query = query.Where(u => u.Estado.idEstado == iEstado);
                    }

                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    var usuarios = lst.Select(item => new UsuarioModel
                    {
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
                        sEstado = item.Estado
                    }).ToList();

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
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasEstado(string sProfecion)
        {
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    if (!string.IsNullOrEmpty(sProfecion) || sProfecion != "0")
                    {
                        query = query.Where(u => u.Usuario.sProfecion == sProfecion);
                    }

                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    var usuarios = lst.Select(item => new UsuarioModel
                    {
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
                        sEstado = item.Estado
                    }).ToList();

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
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasProfesionEstado(string sProfecion, int iEstado)
        {
            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    if (!string.IsNullOrEmpty(sProfecion) || sProfecion != "0")
                    {
                        query = query.Where(u => u.Usuario.sProfecion == sProfecion);
                    }

                    if (iEstado != 0)
                    {
                        query = query.Where(u => u.Estado.idEstado == iEstado);
                    }

                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    var usuarios = lst.Select(item => new UsuarioModel
                    {
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
                        sEstado = item.Estado
                    }).ToList();

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
                // Manejo de errores específicos y retorno de la respuesta personalizada
                if (ex is SqlException)
                {
                    // Error específico de SQL
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 500,
                        Message = "Error en la base de datos: " + ex.Message,
                        Data = null
                    };
                }
                else
                {
                    // Otros errores
                    return new ResponseModel<List<UsuarioModel>>
                    {
                        StatusCode = 404,
                        Message = "No se pudo obtener los datos de la base de datos: " + ex.Message,
                        Data = null
                    };
                }
            }
        }

        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionista(int idProfesionista)
        {
            using (var dbContext = new DB_WSBEntities())
            {
                var lst = (from d in dbContext.tUsuario
                           join m in dbContext.tMunicipio on d.idMunicipio equals m.idMunicipio into municipioGroup
                           from m in municipioGroup.DefaultIfEmpty()
                           join e in dbContext.tEstado on m.idMunicipio equals e.idMunicipio into estadoGroup
                           from e in estadoGroup.DefaultIfEmpty()
                           where (d.idUsuario == idProfesionista) && (d.bEstatus == true)
                           select new
                           {
                               Usuario = d,
                               Municipio = m != null ? m.sMunicipio : null,
                               Estado = e != null ? e.sEstado : null
                           }).ToList();

                var usuarios = lst.Select(item => new UsuarioModel
                {
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

                return new ResponseModel<List<UsuarioModel>>
                {
                    StatusCode = 200,
                    Message = "Datos obtenidos correctamente",
                    Data = usuarios
                };
            }
        }


        [HttpGet]
        public ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario(int idProfesionista)
        {
            using (var dbContext = new DB_WSBEntities())
            {
                var query = from tc in dbContext.tCalendario
                            join td in dbContext.tDia on tc.idDia equals td.idDia
                            where tc.idUsuarioP == idProfesionista
                            && !dbContext.tActaConfirmacionCita.Any(tac => tac.idCalendario == tc.idCalendario)
                            select new CalendarioModel
                            {
                                idCalendario = tc.idCalendario,
                                idUsuarioP = (int)tc.idUsuarioP,
                                idDia = (int)tc.idDia,
                                sHorarioInicio = tc.sHorarioInicio,
                                sHorarioFin = tc.sHorarioFin,
                                iDia = td.iDia
                            };

                var calendarios = query.ToList();

                return new ResponseModel<List<CalendarioModel>>
                {
                    StatusCode = 200,
                    Message = "Datos obtenidos correctamente",
                    Data = calendarios
                };
            }
        }

        [HttpPost]
        public ResponseModel<string> PostCrearACta(int idUsuarioP, int idCalendario, string sMotivo)
        {

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var oACC = new tActaConfirmacionCita();
                    oACC.idUsuarioP = idUsuarioP;
                    oACC.idUsuarioC = 27;
                    oACC.idCalendario = idCalendario;
                    oACC.sMotivo = sMotivo;
                    oACC.bEstatus = 1;

                    dbContext.tActaConfirmacionCita.Add(oACC);
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