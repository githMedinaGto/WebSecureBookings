using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSecureBookings.Controllers.IndexController;

namespace WebSecureBookings.Views.Perfilesprofecionista
{
    public partial class PerfilesProfesionistas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ResponseModel<string>> GetProfesionistas()
        {
            PerfilesProfesionistasController profesionistasController = new PerfilesProfesionistasController();
            var data = profesionistasController.GetProfesionistas();

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

        private static string GenerateTableHtml(List<UsuarioModel> data)
        {
            string result = "";
            foreach (UsuarioModel usuario in data)
            {
                result += $@"
<div class=""col-6 my-4"">       
    <div class=""card"" >
        <div class=""card-body text-center"">
            <h5 class=""card-title"">{usuario.sNombre + " " + usuario.sApellidoP + " " + usuario.sApellidoM}</h5>
            <p class=""card-text"">{usuario.sProfecion}</p>
            <ul class=""list-group list-group-flush"">
                <li class=""list-group-item""><b>Ubicación: </b>{usuario.sCalle + " " + usuario.sColonia + " " + usuario.sMunicipio + " " + usuario.sEstado}</li>
                <li class=""list-group-item""><b>Telefono: </b>{usuario.stelefono}</li>
                <li class=""list-group-item""><b>Area: </b>{usuario.sAreaProfesion}</li>
                <li class=""list-group-item""><b>Correo: </b>{usuario.sCorreo}</li>
            </ul>
            <br/>
            <button type=""button"" class=""btn btn-used"" data-toggle=""modal"" data-target=""#myModal"" onclick=""mostrarInfo({usuario.idUsuario})"">
                Mostrar Info
            </button>
        </div>
    </div>
</div>
";
            }
            return result;
        }
    }
}