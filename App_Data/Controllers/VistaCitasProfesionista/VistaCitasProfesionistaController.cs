using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class VistaCitasProfesionistaController
    {

        public ResponseModel<List<ActaModel>> GetCitasProximas()
        {
            try
            {
                var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                                                               // Decodificar el token para obtener los claims
                Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                string userId = claims[ClaimTypes.NameIdentifier];
                int iIdUser = int.Parse(userId);

                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    var today = DateTime.Today;

                    var citas = (from tacc in dbContext.tActaConfirmacionCita
                                 join tuc in dbContext.tUsuario on tacc.idUsuarioC equals tuc.idUsuario
                                 join tc in dbContext.tCalendario on tacc.idCalendario equals tc.idCalendario
                                 where tacc.idUsuarioP == iIdUser && DbFunctions.TruncateTime(tacc.dFechaRegistro) > today
                                 select new
                                 {
                                     tacc.idActa,
                                     tacc.bEstatus,
                                     tacc.sMotivo,
                                     tuc.sNombre,
                                     tuc.sApellidoP,
                                     tuc.sApellidoM,
                                     tc.sHorarioInicio,
                                     tc.sHorarioFin,
                                     tacc.dFechaRegistro
                                 }).ToList();

                    var citasModel = citas.Select(item => new ActaModel
                    {
                        idActa = item.idActa,
                        bEstatus = (int)item.bEstatus,
                        sMotivo = item.sMotivo,
                        sUsuarioC = $"{item.sNombre} {item.sApellidoP} {item.sApellidoM}",
                        sHora = $"{item.sHorarioInicio} {item.sHorarioFin}",
                        dFechaRegistro = (DateTime)item.dFechaRegistro
                    }).ToList();

                    return new ResponseModel<List<ActaModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = citasModel
                    };
                }
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<ActaModel>>>(ex);

                return new ResponseModel<List<ActaModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }

        public ResponseModel<List<ActaModel>> GetCitas()
        {
            try
            {
                var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                                                               // Decodificar el token para obtener los claims
                Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                string userId = claims[ClaimTypes.NameIdentifier];
                int iIdUser = int.Parse(userId);

                // Establecer una conexión con la base de datos utilizando el contexto de entidad
                using (var dbContext = new DB_WSBEntities())
                {
                    var today = DateTime.Today;

                    var citas = (from tacc in dbContext.tActaConfirmacionCita
                                 join tuc in dbContext.tUsuario on tacc.idUsuarioC equals tuc.idUsuario
                                 join tc in dbContext.tCalendario on tacc.idCalendario equals tc.idCalendario
                                 where tacc.idUsuarioP == iIdUser && DbFunctions.TruncateTime(tacc.dFechaRegistro) == today
                                 select new
                                 {
                                     tacc.idActa,
                                     tacc.bEstatus,
                                     tacc.sMotivo,
                                     tuc.sNombre,
                                     tuc.sApellidoP,
                                     tuc.sApellidoM,
                                     tc.sHorarioInicio,
                                     tc.sHorarioFin,
                                     tacc.dFechaRegistro
                                 }).ToList();

                    var citasModel = citas.Select(item => new ActaModel
                    {
                        idActa = item.idActa,
                        bEstatus = (int)item.bEstatus,
                        sMotivo = item.sMotivo,
                        sUsuarioC = $"{item.sNombre} {item.sApellidoP} {item.sApellidoM}",
                        sHora = $"{item.sHorarioInicio} {item.sHorarioFin}",
                        dFechaRegistro = (DateTime)item.dFechaRegistro
                    }).ToList();

                    return new ResponseModel<List<ActaModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = citasModel
                    };
                }
            }
            catch (Exception ex)
            {
                // En caso de producirse una excepción, manejarla y devolver una respuesta de error personalizada
                var response = ManejoDeErroresModel.Exception<List<List<ActaModel>>>(ex);

                return new ResponseModel<List<ActaModel>>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }

        #region Metodo que obtiene el calendario del profesionista de selección
        [HttpGet]
        public ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario()
        {
            var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
            Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
            string userId = claims[ClaimTypes.NameIdentifier];
            int iIdUser = int.Parse(userId);

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
                            where tc.idUsuarioP == iIdUser
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
                                              $"<button type=\"button\" class=\"btn btn-used float-end\" id=\"btAgendarCita\" onclick=\"JavaScript:fn_AbiriModalGenerarCita({calendario.idDia});\">Actualizar Evento</button>" +
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

        public ResponseModel<string> GuardarFechasHoras(string datos)
        {
            try
            {
                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                                                                   // Decodificar el token para obtener los claims
                    Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                    string userId = claims[ClaimTypes.NameIdentifier];
                    int iIdUser = int.Parse(userId);

                    var lines = datos.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        var dayParts = line.Trim().Split(new[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (dayParts.Length == 2)
                        {
                            var dia = int.Parse(dayParts[0].Trim());
                            var intervalos = dayParts[1].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (var intervalo in intervalos)
                            {
                                var startEndParts = intervalo.Trim().Split(new[] { "Inicio:", "Fin:" }, StringSplitOptions.RemoveEmptyEntries);
                                if (startEndParts.Length == 2)
                                {
                                    var inicio = startEndParts[0].Trim();
                                    var fin = startEndParts[1].Trim();

                                    var oACC = new tCalendario();
                                    oACC.idUsuarioP = iIdUser;
                                    oACC.idDia = dia;
                                    oACC.sHorarioInicio = inicio;
                                    oACC.sHorarioFin = fin;

                                    dbContext.tCalendario.Add(oACC);

                                    dbContext.SaveChanges();
                                }
                            }
                        }
                    }

                    
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
                var response = ManejoDeErroresModel.Exception<List<List<tCalendario>>>(ex);

                return new ResponseModel<string>
                {
                    StatusCode = response.StatusCode,
                    Message = response.Message,
                };
            }
        }

        #region Metodo que actualiza la creación de una acta de cita
        [HttpPost]
        public ResponseModel<string> PostCrearACta(int idCalendario, string sMotivo, string sFecha, int iIdActa)
        {

            try
            {
                var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                Dictionary<string, string> claims = Generar_Token.DecodeToken(token);
                string userId = claims[ClaimTypes.NameIdentifier];
                int iIdUser = int.Parse(userId);
                using (var dbContext = new DB_WSBEntities())
                {
                    // Buscar el registro existente por idUsuarioP e idCalendario
                    var oACCExistente = dbContext.tActaConfirmacionCita
                        .FirstOrDefault(acc => acc.idUsuarioP == iIdUser && acc.idActa == iIdActa);

                    if (oACCExistente != null)
                    {
                        string fechaString = Regex.Replace(sFecha, @"&nbsp;", ""); // Remover los caracteres &nbsp;
                        DateTime fecha = DateTime.ParseExact(fechaString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        // Actualizar las propiedades del registro existente
                        oACCExistente.sMotivo = sMotivo;
                        oACCExistente.idCalendario = idCalendario;
                        oACCExistente.dFechaRegistro = fecha;
                        oACCExistente.bEstatus = 2;

                        // Guardar los cambios en la base de datos
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        return new ResponseModel<string>
                        {
                            StatusCode = 400,
                            Message = "Acta no encontrada"
                        };
                    }
                }

                return new ResponseModel<string>
                {
                    StatusCode = 200,
                    Message = "Datos actualizados exitosamente"
                };
            }
            catch (Exception ex)
            {
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