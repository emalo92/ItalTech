using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Models.TableToExport
{
    public class ConfigurationColumn
    {
        public int Id { get; set; }
        public int OldIndexOrder { get; set; }
        public string OldName { get; set; }
        public int NewIndexOrder { get; set; }
        public string NewName { get; set; }
        public bool IsSelected { get; set; }
    }
}
