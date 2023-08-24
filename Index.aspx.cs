using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSecureBookings.App_Data.Controllers.Autenticacion;

namespace WebSecureBookings
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ResponseModel<string> PostCrearCliente(int sRol, string sNombre, string sApellidoP, string sApellidoM, string sCorreo, string sPassword)
        {
            RegristroUsuariosController registroController = new RegristroUsuariosController();

            return registroController.PostCrearCliente(sRol, sNombre, sApellidoP, sApellidoM, sCorreo, sPassword);
        }


        [WebMethod]
        public static ResponseModel<string> PostCrearProfesionista(int sRol, string sNombre, string sApellidoP, string sApellidoM, 
            string sCorreop, string sPassword01, string sProfesion, string sTelefono, string sArea, string sMunicipio, string sColonia, string sCalle, string sUbicacion
            )
        {
            int iMunicipio = int.Parse(sMunicipio);
            RegristroUsuariosController registroControllerP = new RegristroUsuariosController();
            return registroControllerP.PostCrearProfesionista(sRol, sNombre, sApellidoP, sApellidoM, sCorreop, sPassword01, sProfesion, sTelefono, sArea, iMunicipio, sColonia, sCalle, sUbicacion);
        }

        [WebMethod]
        public static ResponseModel<string> IniciarSesion(string usuario, string contrasena)
        {
            AutenticacionController autenticar = new AutenticacionController();

            return autenticar.IniciarSesion(usuario, contrasena);
        }

        [WebMethod]
        public static List<ResponseModel<string>> GetCiudades()
        {
            AutenticacionController municipiosController = new AutenticacionController();
            var data = municipiosController.ObtenerMunicipios();

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

            string selectOptions = @"<option disabled selected value=0>Seleccione un municipio</option>";

            foreach (MunicipioModel ciudad in data.Data)
            {
                selectOptions += $"<option value=\"{ciudad.idMunicipio}\">{ciudad.sMunicipio}</option>";
            }

            return new List<ResponseModel<string>>
            {
                new ResponseModel<string>
                {
                    StatusCode = data.StatusCode,
                    Message = data.Message,
                    Resultado = selectOptions
                }
            };
        }

        [WebMethod]
        public static List<string> CerrarSesion()
        {
            Generar_Token.RemoveTokenFromCache();// Eliminar el token de la sesión
            List<string> result = new List<string>() {"Cerrando Sesión", "/Index.aspx" };
            return result;
        }
    }
}