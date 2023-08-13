using System;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using WebSecureBookings;

namespace WebSecureBookings
{
    //public class TokenExpirationModule : IHttpModule
    //{
    //    public void Dispose()
    //    {
    //        // Implementa la lógica de limpieza si es necesario
    //    }

    //    public void Init(HttpApplication context)
    //    {
    //        context.BeginRequest += OnBeginRequest;
    //    }

    //    private void OnBeginRequest(object sender, EventArgs e)
    //    {
    //        var application = (HttpApplication)sender;
    //        var context = application.Context;

    //        var token = Generar_Token.GetTokenFromCache(); // Obtener el token desde el caché
    //        if (token != null && Generar_Token.IsTokenExpired(token))
    //        {
    //            context.Response.Redirect("/Views/PerfilesProfesionistas/PerfilesProfesionistas.aspx"); // Redirigir a la página principal
    //        }
    //        else
    //        {
    //            context.Response.Redirect("/Views/RegistroUsuarios/RegistroUsuarios.aspx");
    //        }
    //    }
    //}
}