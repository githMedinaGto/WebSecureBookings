using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSecureBookings.Views.RecuperarCuenta
{
    public partial class RecuperarCuenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ResponseModel<List<UsuarioModel>> BuscarCuenta(string sCorreo)
        {
            RecuperarCuentaController recuperarCuenta = new RecuperarCuentaController();
            return recuperarCuenta.BuscarCuenta(sCorreo);
        }
        [WebMethod]
        public static ResponseModel<List<UsuarioModel>> ActualizarContrasenia(string sCorreo, string sContrasenia, string sToken)
        {
            RecuperarCuentaController recuperarCuenta = new RecuperarCuentaController();
            return recuperarCuenta.ActualizarContrasenia(sCorreo, sContrasenia, sToken);
        }
    }
}