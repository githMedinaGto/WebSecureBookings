using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebSecureBookings
{
    public class ManejoDeErroresModel
    {
        public static ResponseModel<List<T>> Exception<T>(Exception exception)
        {
            // Manejo de errores específicos y retorno de la respuesta personalizada
            if (exception is SqlException)
            {
                // Error específico de SQL
                return new ResponseModel<List<T>>
                {
                    StatusCode = 500,
                    Message = "Error en la base de datos: " + exception.Message,
                    Data = null
                };
            }
            else
            {
                // Otros errores
                return new ResponseModel<List<T>>
                {
                    StatusCode = 404,
                    Message = "No se pudo obtener los datos de la base de datos: " + exception.Message,
                    Data = null
                };
            }
        }

    }
}