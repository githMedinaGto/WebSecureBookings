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
            string sLunes = "<div class=\"tab-pane fade\" id=\"nav-lunes\" role=\"tabpanel\" aria-labelledby=\"nav-lunes-tab\">" +
                "<ul class=\"list-group\" id=\"content-lunes\">";
            string sMartes = "<div class=\"tab-pane fade\" id=\"nav-martes\" role=\"tabpanel\" aria-labelledby=\"nav-martes-tab\">" +
                "<ul class=\"list-group\" id=\"content-martes\">";
            string sMiercoles = "<div class=\"tab-pane fade\" id=\"nav-miercoles\" role=\"tabpanel\" aria-labelledby=\"nav-miercoles-tab\">" +
                "<ul class=\"list-group\" id=\"content-mierccoles\">";
            string sJuevez = "<div class=\"tab-pane fade\" id=\"nav-jueves\" role=\"tabpanel\" aria-labelledby=\"nav-jueves-tab\">" +
                "<ul class=\"list-group\" id=\"content-jueves\">";
            string sViernez = "<div class=\"tab-pane fade\" id=\"nav-viernes\" role=\"tabpanel\" aria-labelledby=\"nav-viernes-tab\">" +
                "<ul class=\"list-group\" id=\"content-viernes\">";
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

                foreach (var calendario in calendarios)
                {
                    string sFecha = GenerarFecha(calendario.iDia);

                    switch (calendario.iDia)
                    {
                        case 1:
                            sLunes += $"<li class=\"list-group-item\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">";
                            sLunes += $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>";
                            sLunes += $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>";
                            sLunes += $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>";
                            sLunes += "<p>Fecha</p>";
                            sLunes += $"<span id=\"txtFecha{calendario.idDia}\">{sFecha}&nbsp;&nbsp</span>";
                            sLunes += $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - ";
                            sLunes += $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>";
                            sLunes += "</li>";
                            break;
                        case 2:
                            sMartes += $"<li class=\"list-group-item\"  onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">";
                            sMartes += $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>";
                            sMartes += $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>";
                            sMartes += $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>";
                            sMartes += "<p>Fecha</p>";
                            sMartes += $"<span id=\"txtFecha{calendario.idDia}\">{sFecha}&nbsp;&nbsp</span>";
                            sMartes += $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - ";
                            sMartes += $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>";
                            sMartes += "</li>";
                            break;
                        case 3:
                            sMiercoles += $"<li class=\"list-group-item\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">";
                            sMiercoles += $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>";
                            sMiercoles += $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>";
                            sMiercoles += $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>";
                            sMiercoles += "<p>Fecha</p>";
                            sMiercoles += $"<span id=\"txtFecha{calendario.idDia}\">{sFecha}&nbsp;&nbsp</span>";
                            sMiercoles += $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - ";
                            sMiercoles += $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>";
                            sMiercoles += "</li>";
                            break;
                        case 4:
                            sJuevez += $"<li class=\"list-group-item\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">";
                            sJuevez += $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>";
                            sJuevez += $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>";
                            sJuevez += $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>";
                            sJuevez += "<p>Fecha</p>";
                            sJuevez += $"<span id=\"txtFecha{calendario.idDia}\">{sFecha}&nbsp;&nbsp</span>";
                            sJuevez += $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - ";
                            sJuevez += $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>";
                            sJuevez += "</li>";
                            break;
                        case 5:
                            sViernez += $"<li class=\"list-group-item\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">";
                            sViernez += $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>";
                            sViernez += $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>";
                            sViernez += $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>";
                            sViernez += "<p>Fecha</p>";
                            sViernez += $"<span id=\"txtFecha{calendario.idDia}\">{sFecha} &nbsp;&nbsp;</span>";
                            sViernez += $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - ";
                            sViernez += $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>";
                            sViernez += "</li>";
                            break;
                        default:
                            sLunes += "</ul>\r\n</div>";
                            sMartes += "</ul>\r\n</div>";
                            sMiercoles += "</ul>\r\n</div>";
                            sJuevez += "</ul>\r\n</div>";
                            sViernez += "</ul>\r\n</div>";
                            break;
                    }
                }

                sLunes += "</ul>\r\n</div>";
                sMartes += "</ul>\r\n</div>";
                sMiercoles += "</ul>\r\n</div>";
                sJuevez += "</ul>\r\n</div>";
                sViernez += "</ul>\r\n</div>";

                string sCalendario = sLunes + sMartes + sJuevez + sViernez;

                return new ResponseModel<List<CalendarioModel>>
                {
                    StatusCode = 200,
                    Message = "Datos obtenidos correctamente",
                    Resultado = sCalendario
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


        public string GenerarFecha(int numeroDia)
        {
            DateTime fecha = DateTime.Now;
            fecha = fecha.AddDays(numeroDia - (int)fecha.DayOfWeek);
            string anio = fecha.Year.ToString();
            string mes = fecha.Month.ToString().PadLeft(2, '0');
            string dia = fecha.Day.ToString().PadLeft(2, '0');
            return anio + '-' + mes + '-' + dia;
        }

    }
}