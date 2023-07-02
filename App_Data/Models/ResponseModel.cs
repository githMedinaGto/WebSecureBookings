using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSecureBookings
{
    public class ResponseModel<T>
    {            
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public string Resultado { get; set; }
    }
}