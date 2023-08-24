using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace WebSecureBookings
{
    public class ActaModel
    {
        public int idActa { get; set; }
        public int idUsuarioP { get; set; }
        public int idUsuarioC { get; set; }
        public int idCalendario { get; set; }
        public int bEstatus { get; set; }
        public string sMotivo { get; set; }
        public string sHora { get; set; }

        public virtual UsuarioModel Usuario { get; set; }
        public virtual CalendarioModel Calendario { get; set; }

        internal string sUsuarioP;

        internal string sUsuarioC;

        internal string AreaProfesion;

        internal DateTime dFechaRegistro;

        internal string stelefono;

        internal string sCorreo;
    }
}