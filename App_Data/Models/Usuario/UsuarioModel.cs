using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using WebSecureBookings.App_Data.Models;

namespace WebSecureBookings
{
    public class UsuarioModel
    {

        public int idUsuario { get; set; }
        public int idRol { get; set; }
	    public Nullable<DateTime> dFechaRegistro { get; set; }
	    public string sNombre { get; set; }
        public string sApellidoP { get; set; }
        public string sApellidoM { get; set; } = null;
        public string sCorreo { get; set; }
        public string stelefono { get; set; }
        public string sProfecion { get; set; } 
        public string sAreaProfesion { get; set; } 
        public int idMunicipio { get; set; }
	    public string sColonia { get; set; }
	    public string sCalle { get; set; }
	    public string sUbicacion { get; set; } = null;
        public bool bEstatus { get; set; }
        public string sToken { get; set; } = null;

        public virtual RolModel Rol { get; set; }
        public virtual MunicipioModel Municipio { get; set; }
        public virtual EstadoModel Estado { get; set; }

        public string sMunicipio;
        public string sEstado;
    }
}