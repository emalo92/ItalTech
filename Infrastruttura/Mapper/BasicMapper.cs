using Infrastruttura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Mapper
{
    public static class BasicMapper
    {
        public static TipoCrud ToDTO(this ItalTech.Models.TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case ItalTech.Models.TipoCrud.insert : return TipoCrud.insert;
                case ItalTech.Models.TipoCrud.delete: return TipoCrud.delete;
                case ItalTech.Models.TipoCrud.update: return TipoCrud.update;
                default : return TipoCrud.update;
            }
        }

        public static ItalTech.Models.TipoCrud ToEntity(this TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case TipoCrud.insert: return ItalTech.Models.TipoCrud.insert;
                case TipoCrud.delete: return ItalTech.Models.TipoCrud.delete;
                case TipoCrud.update: return ItalTech.Models.TipoCrud.update;
                default: return ItalTech.Models.TipoCrud.update;
            }
        }
    }
}
