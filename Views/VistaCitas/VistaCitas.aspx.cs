using System;
using System.Web.Services;
using WebSecureBookings.Controllers.IndexController;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using WebSecureBookings.Controllers.VistaCitasController;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.Views.VistaCitas
{
    public partial class VistaCitas: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ResponseModel<string>> GetCitas()
        {
            VistaCitasController profesionistasController = new VistaCitasController();
            var data = profesionistasController.getObtenerCitas();

            if (data.StatusCode != 200 || data.Data == null || data.Data.Count() == 0)
            {
                return new List<ResponseModel<string>>
                {
                    new ResponseModel<string>
                    {
                        StatusCode = data.StatusCode,
                        Message = data.Message,
                        Resultado = ""
                    }
                };
            }

            string result = GenerateTableHtml(data.Data);

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = result
                }
            };
        }

        private static string GenerateTableHtml(List<ActaModel> data)
        {
            string result = @"<table id=""example"" class=""display"" style=""width:100%"">
        <thead>
            <tr>
                <th>Profesionista</th>
                <th>Área Profesión</th>
                <th>Teléfono</th>
                <th>Horario</th>
                <th>Fecha Registrodate</th>
                <th>Cliente</th>
                <th>Correo</th>
                <th>Numero Cita</th>
                <th>Acción</th>
            </tr>
        </thead>
        <tbody>";

            foreach (ActaModel cita in data)
            {
                result += $@"
            <tr>
                <td>{cita.sUsuarioP}</td>
                <td>{cita.AreaProfesion}</td>
                <td>{cita.stelefono}</td>
                <td>{cita.sHora}</td>
                <td>{cita.dFechaRegistro}</td>
                <td>{cita.sUsuarioC}</td>
                <td>{cita.sCorreo}</td>
                <td>{cita.idActa}</td>";

                if (cita.bEstatus != 2 && cita.bEstatus != 3)
                {
                    DateTime fechaActual = DateTime.Now; // Fecha actual

                    if (cita.dFechaRegistro >= fechaActual)
                    {
                        result += $@"<td> 
                        <button type=""button"" class=""btn btn-used"" onclick=""fn_Calificar({cita.idActa})"">Calificar</button>
                        <button type=""button"" class=""btn btn-secondary"" onclick=""fn_Eliminar({cita.idActa},{cita.idUsuarioP})"">Eliminar</button>
                    </td>";
                    }
                    else
                    {
                        result += $@"<td> 
                        <button type=""button"" class=""btn btn-used"" onclick=""fn_Calificar({cita.idActa})"">Calificar</button>
                        <button type=""button"" class=""btn btn-secondary"" disabled>Eliminar</button>
                    </td>";
                    }
                    
                }
                else
                {
                    result += $@"<td> 
                        <button type=""button"" class=""btn btn-used"" disabled>Calificar</button>
                        <button type=""button"" class=""btn btn-secondary"" disabled>Eliminar</button>
                    </td>";
                }

                result += "</tr>";
            }

            result += @"</tbody></table>";
            return result;
        }

        [WebMethod]
        public static ResponseModel<string> GenComentario(string selectca, string txtArea, string idActa)
        {
            VistaCitasController vistaCitasController = new VistaCitasController();
            return vistaCitasController.GenerarComentario( selectca,  txtArea, idActa);
        }

        [WebMethod]
        public static bool IsCitaCalificada(int idCita)
        {
            using (var dbContext = new DB_WSBEntities()) // Asegúrate de tener el contexto de tu base de datos
            {
                var citaCalificada = dbContext.tActaConfirmacionCita.FirstOrDefault(c => c.idActa == idCita);
                return citaCalificada != null;
            }
        }

        [WebMethod]
        public static ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario(string idUsuario)
        {
            int iIdUsuario = int.Parse(idUsuario);
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            return profesionistasController.GetProfesionistaCalendario(iIdUsuario);
        }

        [WebMethod]
        public static ResponseModel<string> PutAcata(string sMotivo,string sFecha, string idActa, string idCalendario)
        {
            int iIdActa = int.Parse(idActa);
            int iIdCalendario= int.Parse(idCalendario);
            VistaCitasController vistaCitasController = new VistaCitasController();
            return vistaCitasController.PutACta(iIdCalendario, sMotivo, sFecha, iIdActa);
        }

        [WebMethod]
        public static ResponseModel<string> DeleteACta(string sMotivo, string idActa)
        {
            int iIdActa = int.Parse(idActa);
            VistaCitasController vistaCitasController = new VistaCitasController();
            return vistaCitasController.DeleteACta(sMotivo, iIdActa);
        }
    }
}