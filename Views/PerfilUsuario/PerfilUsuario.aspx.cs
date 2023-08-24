using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using WebSecureBookings.Controllers.VistaCitasController;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.Views.PerfilUsuario
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        [WebMethod]
        public static ResponseModel<List<UsuarioModel>> GetUsuarioProfesionista()
        {
            PerfilesUsaurioController usuarioController = new PerfilesUsaurioController();
            var data = usuarioController.GetProfesionistas();

            return new ResponseModel<List<UsuarioModel>>
            {
                StatusCode = data.StatusCode,
                Message = data.Message,
                Data = data.Data
            };
        }

        [WebMethod]
        public static ResponseModel<string> PutUbicacion(string sUbicacion)
        {
            PerfilesUsaurioController perfilesUsaurioController = new PerfilesUsaurioController();
            return perfilesUsaurioController.PutUbicacon(sUbicacion);
        }
        [WebMethod]
        public static ResponseModel<string> PutUsuario(string sProfecion, string sAreaProfesion, string sTelefono,string sCorreo,
                string idMunicipio, string sColonia, string sCalle, string sBEstatus)
        {
            PerfilesUsaurioController perfilesUsaurioController = new PerfilesUsaurioController();
            return perfilesUsaurioController.PutUsuario(sProfecion, sAreaProfesion, sTelefono, sCorreo,
                idMunicipio, sColonia, sCalle, sBEstatus);
        }

    }
}
