using Infrastruttura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Mapper
{
    public static class ProgettazioneMapper
    {

        public static Progetto ToDto (this Data.Modelli.Progetto progetto)
        {
            return new Progetto
            {
                Cliente = progetto.Cliente,
                Codice = progetto.Codice,
                CodiceAnalisiMercato = progetto.CodiceAnalisiMercato,
                CostoFinale = progetto.CostoFinale,
                CostoPrevisto = progetto.CostoPrevisto,
                DataFine = progetto.DataFine,
                DataInizio = progetto.DataInizio,
                Descrizione = progetto.Descrizione,
                NomeProgetto = progetto.NomeProgetto,
                ProjectManager = progetto.ProjectManager,
                Tipo = progetto.Tipo
            };
        }

        public static List<Progetto> ToDto(this List<Data.Modelli.Progetto> progetti)
        {
            List<Progetto> prog = new();
            foreach (var progetto in progetti)
            {
                prog.Add(progetto.ToDto());
            }
            return prog;
        }

    }
}
