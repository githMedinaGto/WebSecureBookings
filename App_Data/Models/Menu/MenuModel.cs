using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSecureBookings
{
    public class MenuModel
    {
        public int idMenu { get; set; }
        public int idRol { get; set; }
	    public string sRuta { get; set; }
	    public string sDescripcion { get; set; }
	    public int idRutaPadre { get; set; }

        public virtual RolModel Rol { get; set; }
    }
}