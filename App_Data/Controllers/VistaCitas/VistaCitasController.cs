using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
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
                        where tacc.idUsuarioC == 14
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
                            tacc.bEstatus
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
                        bEstatus = (int)cita.bEstatus
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
    }
}