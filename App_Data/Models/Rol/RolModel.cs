using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSecureBookings
{
    public class RolModel
    {
        public int idRol { get; set; }
        public string sNomRol { get; set; }
        public string sDescripcion { get; set; } = null;

        public static implicit operator RolModel(List<RolModel> v)
        {
            throw new NotImplementedException();
        }
    }
}