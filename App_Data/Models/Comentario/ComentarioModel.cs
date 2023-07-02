using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class CalemdarioModel
    {
        public int idComentario { get; set; }
        public int idAcata { get; set; }
	    public string sComentario { get; set; } = null;
	    public Nullable<int> iCalificacion { get; set; }

        public ActaModel Acta { get; set; }
    }
}