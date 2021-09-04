using Infrastruttura.Models.Paginations;
using System.Collections.Generic;

namespace Infrastruttura.Models.Paginations
{
    public class ResultPaginated<T>
    {
        public List<T> Result { get; set; }
        public Pagination Pagination { get; set; }
    }
}
