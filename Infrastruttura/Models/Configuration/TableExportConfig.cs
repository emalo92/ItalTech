using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruttura.Models.Configuration
{
    public class TableExportConfig
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string OriginalColumnName { get; set; }
        public int OriginalIndexOrder { get; set; }
        public bool? IsSelected { get; set; }
        public string CurrentColumnName { get; set; }
        public int? CurrentIndexOrder { get; set; }
    }
}
