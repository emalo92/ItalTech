using ItalTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalTech.Mapper
{
    public static class BasicMapper
    {
        public static Infrastruttura.Models.TipoCrud ToDto(this TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case TipoCrud.insert: return Infrastruttura.Models.TipoCrud.insert;
                case TipoCrud.delete: return Infrastruttura.Models.TipoCrud.delete;
                case TipoCrud.update: return Infrastruttura.Models.TipoCrud.update;
                default: return Infrastruttura.Models.TipoCrud.update;
            }
        }
    }
}
