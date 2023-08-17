using System;
using System.Web.Services;
using WebSecureBookings.Controllers.IndexController;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using WebSecureBookings.Controllers.VistaCitasController;

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
            string result = "<table id='tblRoles' class='table table-striped table-bordered table-hover table-sm' style='width: 100%'>";
            result += "<tr>" +
                "<th>Profesionista</th>" +
                "<th>Área Profesión</th>" +
                "<th>Teléfono</th>" +
                "<th>Horario</th>" +
                "<th>Fecha Registro</th>" +
                "<th>Cliente</th>" +
                "<th>Correo</th>" +
                "<th>Numero Cita</th>" +
                "<th>Acción</th>" +
                "</tr>";

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
                <td>{cita.idActa}</td>
                <td> 
                    <button type=""button"" class=""btn btn-success"" id=""btnGenerarCita"" onclick=""fn_Calificar({cita.idActa})"">Calificar</button>
                    <button type=""button"" class=""btn btn-danger"" id=""btnGenerarCita"" onclick=""fn_Eliminar({cita.idActa})"">Eliminar</button>
                </td> 
            </tr>";
            }

            result += "</table>";
            return result;
        }

    }
}