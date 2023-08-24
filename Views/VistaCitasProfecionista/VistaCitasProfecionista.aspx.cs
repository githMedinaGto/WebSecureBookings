using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSecureBookings.Views.VistaCitasProfecionista
{
    public partial class VistaCitasProfecionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ResponseModel<string>> GetCitasProximas()
        {

            VistaCitasProfesionistaController vistaCitasProfesionistaController = new VistaCitasProfesionistaController();
            var data = vistaCitasProfesionistaController.GetCitasProximas();
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
                    Resultado = result,

                }
            };
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetCitas()
        {

            VistaCitasProfesionistaController vistaCitasProfesionistaController = new VistaCitasProfesionistaController();
            var data = vistaCitasProfesionistaController.GetCitas();
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
                    Resultado = result,

                }
            };
        }

        [WebMethod]
        public static ResponseModel<List<CalendarioModel>> GetProfesionistaCalendario()
        {
            VistaCitasProfesionistaController vistaCitasProfesionistaController = new VistaCitasProfesionistaController();
            return vistaCitasProfesionistaController.GetProfesionistaCalendario();
        }
        
        [WebMethod]
        public static ResponseModel<string> GenerarFechasHoras(string datos)
        {
            VistaCitasProfesionistaController vistaCitasProfesionistaController = new VistaCitasProfesionistaController();
            return vistaCitasProfesionistaController.GuardarFechasHoras(datos);
        }

        [WebMethod]
        public static ResponseModel<string> PostAcAcata( string sMotivo, string idCalendario, string sFecha, string idActa)
        {
            int iIdCalendario =int.Parse(idCalendario);
            int iIdActa = int.Parse(idActa);
            VistaCitasProfesionistaController vistaCitasProfesionistaController = new VistaCitasProfesionistaController();
            return vistaCitasProfesionistaController.PostCrearACta(iIdCalendario, sMotivo, sFecha, iIdActa);
        }

        private static string GenerateTableHtml(List<ActaModel> data)
        {
            string result = @"<table id=""example"" class=""display"" style=""width:100%"">
        <thead>
            <tr>
                <th>Cliente</th>
                <th>Fecha</th>
                <th>Horario</th>
                <th>Estatus</th>
                <th>Editar Fecha</th>
            </tr>
        </thead>
        <tbody>";

            foreach (ActaModel cita in data)
            {
                string fecha = ((DateTime)cita.dFechaRegistro).ToString("yyyy-MM-dd");
                string sEsTado = "";

                // Formato de fecha sin hora
                if (cita.bEstatus == 0)
                {
                    sEsTado = "Inactivo";
                }
                else if (cita.bEstatus == 1)
                {
                    sEsTado = "Activo";
                }
                else
                {
                    sEsTado = "Actualizado";
                }

                result += $@"
            <tr>
                <td>{cita.sUsuarioC}</td>
                <td>{fecha}</td>
                <td>{cita.sHora}</td>
                <td>{sEsTado}</td>
                <td><i class='fa fa-calendar' onclick='fn_cambiarfecha({cita.idActa})' onmouseover='this.style.cursor=&#39;pointer&#39;'></i></td>";

                

                result += " </tr>";
            }

            result += @"</tbody></table>";
            return result;
        }

    }

    public class DiaConCitas
    {
        public List<Cita> lunes { get; set; }
        public List<Cita> martes { get; set; }
        public List<Cita> miercoles { get; set; }
        public List<Cita> jueves { get; set; }
        public List<Cita> viernes { get; set; }
    }

    public class Cita
    {
        public string inicio { get; set; }
        public string fin { get; set; }
    }
}