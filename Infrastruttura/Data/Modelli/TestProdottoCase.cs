using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class TestProdottoCase
    {
        public int CodiceTest { get; set; }
        public int CodiceProdottoCase { get; set; }

        public virtual ProdottoCase CodiceProdottoCaseNavigation { get; set; }
        public virtual Test CodiceTestNavigation { get; set; }
    }
}
