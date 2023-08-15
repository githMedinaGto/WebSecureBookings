using System;
using System.Web.Services;
using WebSecureBookings.Controllers.IndexController;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Web.Caching;
using System.Runtime.Caching;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebSecureBookings.Views.Index
{
    public partial class Index : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<ResponseModel<string>> GetTablaRoles()
        {
            IndexController indexController = new IndexController();
            var data = indexController.GetRolDataTable();

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

            string[] aColumnas = new string[] { "ROL", "DESCRIPCION", "ACCIONES" };
            string result = GenerateTableHtml(data.Data, aColumnas);

            // Crea una instancia de la clase que contiene el método GetTokenFromCache()
            var index = new Index();
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


        public string GetTokenFromCache()
        {
            var cache = MemoryCache.Default;
            // Obtén el token actual desde el caché
            var token = cache.Select(kvp => kvp.Key).FirstOrDefault() as string;

            // Verifica si se encontró un token en el caché
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }

            // Si no se encontró ningún token en el caché, devuelve null o maneja el caso según tus necesidades
            return null;
        }

        private static string GenerateTableHtml(List<RolModel> data, string[] aColumnas)
        {
            string result = "";
            result += "<table id='tblRoles' class='table table-striped table-bordered table-hover table-sm' style='width: 100%'> " +
                            "<thead>" +
                            "<tr>";
            foreach (string sColumna in aColumnas)
            {
                result += "<th>" + sColumna + "</th>";
            }
            result += "</tr></thead>";
            result += "<tbody>";

            foreach (RolModel rol in data)
            {
                result += "<tr>";
                result += "<td>" + rol.sNomRol.ToString() + "</td>";
                result += "<td>" + rol.sDescripcion.ToString() + "</td>";

                result += "<td>" +
                    "<span class='fa fa-pencil-square-o' style='color:#85c555;font-size: 35px;  cursor: pointer;' onclick='javascript:fn_SeleccionarRol(" + rol.idRol.ToString() + ");'></span>" +
                    "&nbsp;&nbsp;" +
                    "<span class='fa fa-trash-o' style='color:red;font-size: 35px;  cursor: pointer;' onclick='javascript:fn_EliminarEquipo(" + rol.idRol.ToString() + ");'></span>" +
                    "</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "<tfoot>" +
                "<tr>";
            result += "</tr>" +
                "</tfoot>" +
                "</table>";

            return result;
        }


        [WebMethod]
        public static ResponseModel<string> PostCrearRol(string sRol, string sDescripcion)
        {
            IndexController indexController = new IndexController();
            return indexController.PostCrearRol(sRol, sDescripcion);
        }

        [WebMethod]
        public static ResponseModel<List<RolModel>> GetRol(string idRol){
            IndexController indexController = new IndexController();
            return indexController.GetRolId(idRol);
        }

        [WebMethod]
        public static ResponseModel<string> PutRol(string idRol, string sRol, string sDescripcion)
        {
            IndexController indexController = new IndexController();
            return indexController.PutRol(idRol, sRol, sDescripcion);
        }

        [WebMethod]
        public static ResponseModel<List<RolModel>> DeleteRol(string idRol)
        {
            IndexController indexController = new IndexController();
            return indexController.DeleteRolId(idRol);
        }

        [WebMethod]
        public static string GenerarUsuarioToken()
        {
            try
            {
                var userId = "123456";
                var role = "admin";

                var token = Generar_Token.GenerateToken(userId, role);
                var obtenerToken = Generar_Token.GetTokenFromCache();
                var inforToken = Generar_Token.DecodeToken(obtenerToken);
                string url = Generar_Token.RedirecAPaginaPrincipal(obtenerToken);
                return url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}