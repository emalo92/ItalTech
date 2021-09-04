using System.Collections.Generic;

namespace Infrastruttura.Models.Paginations
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public bool IsPaginated { get; set; } = true;
        public string OrderBy { get; set; }
        public bool OrderAscending { get; set; }
        public Route Route { get; set; }
        public Dictionary<string, dynamic> ParametriRicerca { get; set; }
        public string JavascriptNavigationMethod { get; set; }
    }
}
