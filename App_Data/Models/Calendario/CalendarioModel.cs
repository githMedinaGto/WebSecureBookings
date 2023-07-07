using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSecureBookings
{
    public class CalendarioModel
    {
        public int idCalendario { get; set; }
        public int idUsuarioP { get; set; }
        public int idDia { get; set; }
	    public string sHorarioInicio { get; set; }
	    public string sHorarioFin { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual DiaModel Dia { get; set; }
        public int iDia { get; internal set; }
    }
}