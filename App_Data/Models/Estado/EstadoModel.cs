using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSecureBookings
{
    public class EstadoModel
    {
        public int idEstado { get; set; }
        public int idMunicipio { get; set; }
	    public string sEstado { get; set; }

        public virtual MunicipioModel Municipio { get; set; }

        public static implicit operator EstadoModel(List<EstadoModel> v)
        {
            throw new NotImplementedException();
        }
    }
}