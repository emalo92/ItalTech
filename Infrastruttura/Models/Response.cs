
namespace Infrastruttura.Models
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
