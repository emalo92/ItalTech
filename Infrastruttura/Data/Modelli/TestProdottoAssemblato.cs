using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class TestProdottoAssemblato
    {
        public int CodiceTest { get; set; }
        public int CodiceProdottoAssemblato { get; set; }

        public virtual ProdottoAssemblato CodiceProdottoAssemblatoNavigation { get; set; }
        public virtual Test CodiceTestNavigation { get; set; }
    }
}
