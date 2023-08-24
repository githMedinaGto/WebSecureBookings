﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public partial class Master : System.Web.UI.MasterPage
    {
        private bool redirectPerformed = false; // Bandera para evitar múltiples redirecciones

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!redirectPerformed) // Verificar si la redirección ya se ha realizado
            {
                string currentUrl = HttpContext.Current.Request.Url.PathAndQuery;
                Uri referrer = HttpContext.Current.Request.UrlReferrer;
                string referringUrl = referrer != null ? referrer.PathAndQuery : string.Empty;


                string token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
                if (token != null && Generar_Token.IsTokenExpired(token))
                {
                    var menuElement = FindControl("menu") as HtmlGenericControl;
                    menuElement.Controls.Clear();
                    Generar_Token.RemoveTokenFromCache(); // Eliminar el token de la sesión
                    Response.Redirect("/Index.aspx"); // Redirigir a la página principal
                    redirectPerformed = true; // Marcar que la redirección se ha realizado
                }
                else if (token != null)
                {
                    AccesoDeUsuario(currentUrl, referringUrl);
                    var menuElement = FindControl("menu") as HtmlGenericControl;

                    List<string> rutas = GenerarMenu(token);

                    // Limpiar el menú existente eliminando todos los elementos hijos
                    menuElement.Controls.Clear();

                    foreach (var ruta in rutas)
                    {
                        var listItem = new HtmlGenericControl("li");
                        listItem.Attributes["class"] = "nav-item"; // Agregar la clase "nav-item" al elemento <li>

                        var link = new HtmlGenericControl("a");
                        link.Attributes["class"] = "nav-link text-white"; // Agregar la clase "nav-link" al elemento <a>
                        link.Attributes["href"] = ruta;
                        link.InnerText = ObtenerTextoMenu(ruta);

                        listItem.Controls.Add(link);
                        menuElement.Controls.Add(listItem);
                    }


                }
                else if (token == null)
                {
                    Response.Redirect("/Index.aspx"); // Redirigir a la página principal
                }
            }
        }

        public List<string> GenerarMenu(string token)
        {
            List<string> rutas = new List<string>();
            Dictionary<string, string> claims = Generar_Token.DecodeToken(token); // Decodificar el token y obtener las reclamaciones

            string role = claims[ClaimTypes.Role]; // Obtener el valor del reclamo de tipo "ClaimTypes.Role"
            int iRol = int.Parse(role);

            try
            {
                using (var dbContext = new DB_WSBEntities())
                {
                    var lst = (from d in dbContext.tMenu
                               where d.idRol == iRol
                               select new MenuModel
                               {
                                   sRuta = d.sRuta
                               }).ToList();

                    foreach (var item in lst)
                    {
                        rutas.Add(item.sRuta);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rutas;
        }

        public string ObtenerTextoMenu(string ruta)
        {
            using (var dbContext = new DB_WSBEntities())
            {
                string result = dbContext.tMenu
                .Where(d => d.sRuta == ruta)
                .Select(d => d.sDescripcion)
                .FirstOrDefault(); // Obtén solo el primer resultado o null si no hay coincidencias

                return result;
            }
        }

        public void AccesoDeUsuario(string currentUrl, string referringUrl)
        {
            string token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
            List<string> rutas = new List<string>();
            Dictionary<string, string> claims = Generar_Token.DecodeToken(token); // Decodificar el token y obtener las reclamaciones

            string role = claims[ClaimTypes.Role]; // Obtener el valor del reclamo de tipo "ClaimTypes.Role"
            int iRol = int.Parse(role);
            string result;

            using (var dbContext = new DB_WSBEntities())
            {
                result = dbContext.tMenu
                .Where(d => d.idRol == iRol & d.sRuta == currentUrl)
                .Select(d => d.sDescripcion)
                .FirstOrDefault(); // Obtén solo el primer resultado o null si no hay coincidencias
            }

            if(result != null && result != "") {
                
            }
            else
            {
                if(referringUrl != null & referringUrl != "")
                {
                    Response.Redirect(referringUrl);
                }
                else
                {
                    string url = "";

                    if (iRol == 2)
                    {
                        url = "/Views/PerfilesProfecionista/PerfilesProfesionistas.aspx";
                    }
                    else
                    {
                        url = "/Views/VistaCitasProfecionista/VistaCitasProfecionista.aspx";
                    }
                    Response.Redirect(url);
                }
                
            }
        }
    }
}