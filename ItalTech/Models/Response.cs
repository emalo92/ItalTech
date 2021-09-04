using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool IsSucces { get; set; }
        public dynamic Result { get; set; }
    }

    public class Response<T>
    {
        public string Message { get; set; }
        public bool IsSucces { get; set; }
        public T Result { get; set; }
    }
}
