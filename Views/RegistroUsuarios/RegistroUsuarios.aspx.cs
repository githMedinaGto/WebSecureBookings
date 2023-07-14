using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSecureBookings.Views.RegistroUsuarios
{
    public partial class RegistroUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ResponseModel<string> PostCrearCliente(int sRol, string sApellidoP, string sApellidoM, string sCorreo, string sPassword,string sNombre)
        {
            RegristroUsuariosController registroController = new RegristroUsuariosController();

            return registroController.PostCrearCliente(sRol, sApellidoP, sApellidoM, sCorreo, sPassword, sNombre);
        }


        [WebMethod]
        public static ResponseModel<string> PostCrearProfesionista(int sRol, string sNombre, string sApellidoP, string sApellidoM, string sCorreop, string sPassword01, string sProfesion, string sTelefono, string sArea, int sMunicipio, string sColonia, string sCalle, string sUbicacion)
        {
            RegristroUsuariosController registroControllerP = new RegristroUsuariosController();

            return registroControllerP.PostCrearProfesionista(sRol, sNombre, sApellidoP, sApellidoM, sCorreop, sPassword01, sProfesion, sTelefono, sArea, sMunicipio, sColonia, sCalle, sUbicacion);
        }
    }
}