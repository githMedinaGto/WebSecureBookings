using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings.Views.PerfilUsuario
{
    public partial class PerfilUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        [WebMethod]
        public static ResponseModel<List<UsuarioModel>> GetUsuarioProfesionista()
        {
            RegristroUsuariosController usuarioController = new RegristroUsuariosController();
            var data = usuarioController.GetProfesionista();

            return new ResponseModel<List<UsuarioModel>>
            {
                StatusCode = data.StatusCode,
                Message = data.Message,
                Data = data.Data
            };

        }

    }
    
}
