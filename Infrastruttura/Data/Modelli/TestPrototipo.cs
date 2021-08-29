using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastruttura.Data.Modelli
{
    public partial class TestPrototipo
    {
        public int CodiceTest { get; set; }
        public int NumPrototipo { get; set; }
        public int CodiceProgetto { get; set; }

        public virtual Test CodiceTestNavigation { get; set; }
        public virtual Prototipo Prototipo { get; set; }
    }
}
