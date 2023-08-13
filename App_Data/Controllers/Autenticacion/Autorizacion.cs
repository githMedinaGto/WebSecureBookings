using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class Autorizacion : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Obtener roles del usuario (desde la sesión o cookie)
                string[] roles = GetRolesForUser(filterContext.HttpContext.User.Identity.Name);

                // Verificar si el usuario tiene el rol necesario
                if (!roles.Any(r => Roles.Contains(r)))
                {
                    // El usuario no tiene el rol necesario para acceder a la página
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
            else
            {
                // El usuario no está autenticado, redirigir al login
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }

        private string[] GetRolesForUser(string username)
        {
            // Aquí debes implementar la lógica para obtener los roles del usuario
            using (var dbContext = new DB_WSBEntities())
            {
                var user = dbContext.tUsuario.FirstOrDefault(u => u.sNombre == username);
                if (user != null)
                {
                    var roleId = user.idRol;
                    var role = dbContext.tRol.FirstOrDefault(r => r.idRol == roleId);
                    if (role != null)
                    {
                        return new string[] { role.sNomRol };
                    }
                }
            }

            return new string[0];
        }
    }
}