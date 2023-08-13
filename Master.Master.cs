using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSecureBookings
{
    public partial class Master : System.Web.UI.MasterPage
    {
        private bool redirectPerformed = false; // Bandera para evitar múltiples redirecciones

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!redirectPerformed) // Verificar si la redirección ya se ha realizado
            {
                string token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                if (token != null && Generar_Token.IsTokenExpired(token))
                {
                    Response.Redirect("/Views/RegistroUsuarios/RegistroUsuarios.aspx"); // Redirigir a la página principal
                    redirectPerformed = true; // Marcar que la redirección se ha realizado
                }
            }
        }
    }
}