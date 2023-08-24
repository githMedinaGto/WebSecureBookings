using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using WebSecureBookings.App_Data.Models;


namespace WebSecureBookings.Controllers.VistaCitasController
{
    public class VistaCitasController
    {
        public ResponseModel<List<ActaModel>> getObtenerCitas()
        {
            var token = Generar_Token.GetTokenFromCache();
            Dictionary<string, string> i = Generar_Token.DecodeToken(token);
            string userid = i[ClaimTypes.NameIdentifier];
            int iduser = int.Parse(userid);
            try
            {
                using (var dbContext = new DB_WSBEntities()) {
                    var citasConfirmadas = (
                       from tacc in dbContext.tActaConfirmacionCita
                       join tu in dbContext.tUsuario on tacc.idUsuarioP equals tu.idUsuario
                        join tc in dbContext.tCalendario on tacc.idCalendario equals tc.idCalendario
                        join tuc in dbContext.tUsuario on tacc.idUsuarioC equals tuc.idUsuario
                        where tacc.idUsuarioC == iduser
                        select new
                          {
                            Profesionista = tu.sNombre + " " + (tu.sApellidoP ?? "") + " " + (tu.sApellidoM ?? ""),
                            tu.sAreaProfesion,
                            tu.stelefono,
                            Horario = tc.sHorarioInicio + "-" + tc.sHorarioFin,
                            tacc.dFechaRegistro,
                            Cliente = tuc.sNombre + " " + (tuc.sApellidoP ?? "") + " " + (tuc.sApellidoM ?? ""),
                            tuc.sCorreo,
                            tacc.idActa,
                            tacc.bEstatus,
                            tacc.idUsuarioP
                        }
                     ).ToList();

                    // Ahora puedes mapear los resultados a un modelo C# si lo deseas
                    var citasConfirmadasModel = citasConfirmadas.Select(cita => new ActaModel
                    {
                        sUsuarioP = cita.Profesionista,
                        AreaProfesion = cita.sAreaProfesion,
                        stelefono = cita.stelefono,
                        sHora = cita.Horario,
                        dFechaRegistro = cita.dFechaRegistro != null ? (DateTime)cita.dFechaRegistro : DateTime.MinValue,
                        sUsuarioC = cita.Cliente,
                        sCorreo = cita.sCorreo,
                        idActa = cita.idActa,
                        bEstatus = (int)cita.bEstatus,
                        idUsuarioP = (int)cita.idUsuarioP
                    }).ToList();

                    // Devolver una respuesta exitosa con los datos obtenidos
                    return new ResponseModel<List<ActaModel>>
                    {
                        StatusCode = 200,
                        Message = "Datos obtenidos correctamente",
                        Data = citasConfirmadasModel
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

        #region Metodo que genera los comentarios de los usuarios
        [HttpPost]
        public ResponseModel<string> GenerarComentario(string selectca, string txtArea, string idActa)
        {

            try
            {
                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    int iCal = int.Parse(selectca); 
                    int iIdActa = int.Parse(idActa);
                    var tacc = new tCometario();

                    // Asignar valores a las propiedades de la nueva entrada en la tabla
                    tacc.idAcata = iIdActa; 
                    tacc.sComentario = txtArea; // Motivo de la confirmación
                    tacc.iCalificacion = iCal; 

                    // Agregar la nueva entrada a la tabla tActaConfirmacionCita
                    dbContext.tCometario.Add(tacc);
                    // Guardar los cambios en la base de datos
                    dbContext.SaveChanges();
                }
                using (var dbContext = new DB_WSBEntities())
                {
                    int iIdActa = int.Parse(idActa);
                    var upActa = dbContext.tActaConfirmacionCita.Find(iIdActa);
                    //El 2 Indica que ya se comento la consulta de la cita
                    upActa.bEstatus = 2;
                    dbContext.Entry(upActa).State = System.Data.Entity.EntityState.Modified;
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


        #region Metodo que actualiza una acta de cita
        [HttpPost]
        public ResponseModel<string> PutACta(int idCalendario, string sMotivo, string sFecha, int iIdActa)
        {

            try
            {
                string cadenaSinEspacios = sFecha.Replace("&nbsp;&nbsp;", "");
                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    var upActa = dbContext.tActaConfirmacionCita.Find(iIdActa);
                    //El 3 Indica que ya se comento la consulta de la cita
                    upActa.sMotivo = sMotivo; // Motivo de la confirmación
                    upActa.dFechaRegistro = DateTime.ParseExact(cadenaSinEspacios, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    // Asignar valores a las propiedades de la nueva entrada en la tabla
                    upActa.idCalendario = idCalendario; // ID del calendario asociado a la confirmación
                    upActa.bEstatus = 3;
                    dbContext.Entry(upActa).State = System.Data.Entity.EntityState.Modified;
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

        #region Metodo que actualiza una acta de cita
        [HttpPost]
        public ResponseModel<string> DeleteACta(string sMotivo, int iIdActa)
        {

            try
            {

                // Usar el contexto de la base de datos con una declaración "using" para asegurarse de que se libere correctamente
                using (var dbContext = new DB_WSBEntities())
                {
                    var upActa = dbContext.tActaConfirmacionCita.Find(iIdActa);
                    //El 3 Indica que ya se comento la consulta de la cita
                    upActa.sMotivo = sMotivo; // Motivo de la confirmación
                    upActa.bEstatus = 0;
                    dbContext.Entry(upActa).State = System.Data.Entity.EntityState.Modified;
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