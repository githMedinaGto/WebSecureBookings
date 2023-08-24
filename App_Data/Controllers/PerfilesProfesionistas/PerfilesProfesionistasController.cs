using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class PerfilesProfesionistasController
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
                    // Realizar una consulta para obtener una lista de usuarios con información adicional
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

                    // Mapear los resultados de la consulta a objetos de modelo de usuario
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

        #region Metodo que obtiene un listado de las diferentes profesiones que se tienen 
        [HttpGet]
        public ResponseModel<List<string>> GetProfesiones()
        {

            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Realizar una consulta para obtener una lista de profesiones distintas de los usuarios activos con el rol 1
                    var profesiones = (from d in dbContext.tUsuario
                                       where d.bEstatus == true && d.idRol == 1
                                       select d.sProfecion).Distinct().ToList();

                    // Devolver una respuesta exitosa con los datos obtenidos
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
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<List<string>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }
        #endregion

        #region Metodo que obtiene un listado de las diferentes ciudades que se tienen
        [HttpGet]
        public ResponseModel<List<EstadoModel>> GetCiudades()
        {

            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Realizar una consulta para obtener una lista de estados y mapearlos a objetos de modelo de estado
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
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<UsuarioModel>>>(ex);

                return new ResponseModel<List<EstadoModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }
        #endregion

        #region Metodo que obtiene los profesionistas por estado(Ciudad)
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasEstado( int iEstado)
        {
            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Construir la consulta para obtener los profesionistas por estado (ciudad)
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    // Aplicar el filtro por estado si se proporciona un valor distinto de cero
                    if (iEstado != 0)
                    {
                        query = query.Where(u => u.Estado.idEstado == iEstado);
                    }

                    // Ejecutar la consulta y obtener los resultados
                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    // Mapear los resultados a objetos de modelo de usuario
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

        #region Metodo que optiene los profesionistas por profesión
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasEstado(string sProfecion)
        {
            try
            {
                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    // Consulta para obtener los profesionistas que están activos y tienen el rol 1 (rol de profesionistas)
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    // Si se proporciona una profesión específica, aplicar un filtro para obtener solo profesionistas con esa profesión
                    if (!string.IsNullOrEmpty(sProfecion) || sProfecion != "0")
                    {
                        query = query.Where(u => u.Usuario.sProfecion == sProfecion);
                    }

                    // Obtener los resultados de la consulta
                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    // Mapear los resultados obtenidos a objetos UsuarioModel
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

                    // Devolver una respuesta exitosa con los profesionistas encontrados
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

        #region Metodo que obtiene profesionistas por profesion y estado
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionistasProfesionEstado(string sProfecion, int iEstado)
        {
            try
            {
                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    // Consulta para obtener los profesionistas que están activos y tienen el rol 1 (probablemente rol de profesionistas)
                    var query = dbContext.tUsuario
                        .Join(dbContext.tMunicipio, d => d.idMunicipio, m => m.idMunicipio, (d, m) => new { Usuario = d, Municipio = m })
                        .Join(dbContext.tEstado, dm => dm.Municipio.idMunicipio, e => e.idMunicipio, (dm, e) => new { Usuario = dm.Usuario, Municipio = dm.Municipio, Estado = e })
                        .Where(u => u.Usuario.bEstatus == true && u.Usuario.idRol == 1);

                    // Si se proporciona una profesión específica, aplicar un filtro para obtener solo profesionistas con esa profesión
                    if (!string.IsNullOrEmpty(sProfecion) || sProfecion != "0")
                    {
                        query = query.Where(u => u.Usuario.sProfecion == sProfecion);
                    }

                    // Si se proporciona un estado específico, aplicar un filtro para obtener solo profesionistas de ese estado
                    if (iEstado != 0)
                    {
                        query = query.Where(u => u.Estado.idEstado == iEstado);
                    }

                    // Obtener los resultados de la consulta
                    var lst = query
                        .Select(item => new
                        {
                            Usuario = item.Usuario,
                            Municipio = item.Municipio != null ? item.Municipio.sMunicipio : null,
                            Estado = item.Estado != null ? item.Estado.sEstado : null
                        })
                        .ToList();

                    // Mapear los resultados obtenidos a objetos UsuarioModel
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

                    // Devolver una respuesta exitosa con los profesionistas encontrados
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

        #region Metodo que optiene un profesionista por idProfesionista
        [HttpGet]
        public ResponseModel<List<UsuarioModel>> GetProfesionista(int idProfesionista)
        {
            // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
            using (var dbContext = new DB_WSBEntities())
            {
                // Realizar una consulta para obtener el profesionista con el id proporcionado
                var lst = (from d in dbContext.tUsuario
                           join m in dbContext.tMunicipio on d.idMunicipio equals m.idMunicipio into municipioGroup
                           from m in municipioGroup.DefaultIfEmpty()
                           join e in dbContext.tEstado on m.idMunicipio equals e.idMunicipio into estadoGroup
                           from e in estadoGroup.DefaultIfEmpty()
                           // Aplicar filtros para obtener solo el profesionista con el id y que esté activo
                           where (d.idUsuario == idProfesionista) && (d.bEstatus == true)
                           select new
                           {
                               Usuario = d,
                               Municipio = m != null ? m.sMunicipio : null,
                               Estado = e != null ? e.sEstado : null
                           }).ToList();

                // Mapear los resultados obtenidos a objetos UsuarioModel
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
        #endregion

        #region Metodo que obtiene el calendario del profesionista de selección
        [HttpGet]
        public ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario(int idProfesionista)
        {
            // Inicializar strings que almacenarán el contenido HTML del calendario por días de la semana
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

            // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
            using (var dbContext = new DB_WSBEntities())
            {
                // Realizar una consulta para obtener el calendario de disponibilidad del profesionista con el id proporcionado
                var query = from tc in dbContext.tCalendario
                            join td in dbContext.tDia on tc.idDia equals td.idDia
                            where tc.idUsuarioP == idProfesionista
                            // Filtrar solo los registros que no tengan una confirmación de cita asociada
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

                string[] diasSemana = { sLunes, sMartes, sMiercoles, sJuevez, sViernez };

                DateTime hoy = DateTime.Today;
                int numeroDia = (int)hoy.DayOfWeek;

                for (int i = 0; i < calendarios.Count; i++)
                {
                    var calendario = calendarios[i];
                    string sFecha = GenerarFecha(calendario.iDia);

                    if (calendario.iDia >= 1 && calendario.iDia <= 5 && numeroDia <= calendario.iDia)
                    {
                        string contenidoHtml = $"<li class=\"list-group-item\" >" +
                                              $"<span id=\"txtIdCalendario{calendario.idDia}\" style=\"display: none;\">{calendario.idCalendario}</span>" +
                                              $"<span id=\"txtIdUsuProfesionista{calendario.idDia}\" style=\"display: none;\">{calendario.idUsuarioP}</span>" +
                                              $"<p id=\"txtTitulo{calendario.idDia}\"><b>Disponible</b></p>" +
                                              "<p>Fecha</p>" +
                                              $"<span id=\"txtFecha{calendario.idDia}\">{sFecha}&nbsp;&nbsp;</span>" +
                                              $"<span id=\"txtHoaraInicio{calendario.idDia}\">{calendario.sHorarioInicio}</span> - " +
                                              $"<span id=\"txtHoraFechaFin{calendario.idDia}\">{calendario.sHorarioFin}</span>" +
                                              $"<button type=\"button\" class=\"btn btn-used float-end\" id=\"btAgendarCita\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">Generar Evento</button>" +
                                              "</li>";

                        diasSemana[calendario.iDia - 1] += contenidoHtml;
                    }
                }

                // Cerrar las etiquetas de los contenedores de los días de la semana
                for (int i = 0; i < diasSemana.Length; i++)
                {
                    diasSemana[i] += "</ul>\r\n</div>";
                }

                // Unir todos los strings para obtener el resultado final con el contenido HTML completo del calendario
                string sCalendario = string.Concat(diasSemana);

                // Devolver una respuesta exitosa con el calendario generado en formato HTML
                return new ResponseModel<List<CalendarioModel>>
                {
                    StatusCode = 200,
                    Message = "Datos obtenidos correctamente",
                    Resultado = sCalendario
                };

            }
        }
        #endregion

        #region Metodo que genera la creación de una acta de cita
        [HttpPost]
        public ResponseModel<string> PostCrearACta(int idUsuarioP, int idCalendario, string sMotivo, string sFecha)
        {

            try
            {
                var token = Generar_Token.GetTokenFromCache();
                Dictionary<string, string> i = Generar_Token.DecodeToken(token);
                string userid = i[ClaimTypes.NameIdentifier];
                int iduser = int.Parse(userid);
                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    // Crear una nueva instancia de tActaConfirmacionCita
                    var oACC = new tActaConfirmacionCita();

                    // Asignar valores a las propiedades de la nueva entrada en la tabla
                    oACC.idUsuarioP = idUsuarioP; // ID del profesionista
                    oACC.idUsuarioC = iduser; // ID del usuario (quién crea la confirmación). Es fijo 14 o debería ser obtenido de alguna otra manera
                    oACC.idCalendario = idCalendario; // ID del calendario asociado a la confirmación
                    oACC.sMotivo = sMotivo; // Motivo de la confirmación
                    oACC.dFechaRegistro = DateTime.ParseExact(sFecha, "yyyy-MM-dd h:mm tt", CultureInfo.InvariantCulture);
                    oACC.bEstatus = 1; // ¿Es 1 el valor correcto para indicar que la confirmación está activa?

                    // Agregar la nueva entrada a la tabla tActaConfirmacionCita
                    dbContext.tActaConfirmacionCita.Add(oACC);
                    // Guardar los cambios en la base de datos
                    dbContext.SaveChanges();
                }

                // Devolver una respuesta exitosa
                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos guardados exitosamente"
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

        #region Metodo que genera la fecha por el numero del dia
        public string GenerarFecha(int numeroDia)
        {
            // Obtener la fecha actual
            DateTime fecha = DateTime.Now;

            // Calcular la fecha del día de la semana deseado restando el número de días desde el día actual
            fecha = fecha.AddDays(numeroDia - (int)fecha.DayOfWeek);

            // Extraer el año, el mes y el día de la fecha resultante
            string anio = fecha.Year.ToString();
            string mes = fecha.Month.ToString().PadLeft(2, '0'); // Asegurar que el mes tenga dos dígitos (01, 02, ..., 12)
            string dia = fecha.Day.ToString().PadLeft(2, '0'); // Asegurar que el día tenga dos dígitos (01, 02, ..., 31)

            // Combinar los elementos para formar la cadena de fecha en formato AAAA-MM-DD
            return anio + '-' + mes + '-' + dia;
        }
        #endregion
    }
}