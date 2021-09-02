using ItalTech.Areas.Progettazione.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Mapper
{
    public static class ProgettazioneMapper
    {
        public static Progetto ToModel (this Infrastruttura.Models.Progetto progetto)
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

        public static Infrastruttura.Models.Progetto ToDto(this Progetto progetto)
        {
            return new Infrastruttura.Models.Progetto
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

        public static List<Progetto> ToModel(this List<Infrastruttura.Models.Progetto> progettiDal)
        {
            var progetti = new List<Progetto>();
            foreach (var progetto in progettiDal)
            {
                progetti.Add(progetto.ToModel());
            }
            return progetti;
        }

        public static List<Infrastruttura.Models.Progetto> ToModel(this List<Progetto> progettiDal)
        {
            var progetti = new List<Infrastruttura.Models.Progetto>();
            foreach (var progetto in progettiDal)
            {
                progetti.Add(progetto.ToDto());
            }
            return progetti;
        }

        public static Infrastruttura.Models.Input.InputRicercaProgetti ToDto(this InputRicercaProgetti input)
        {
            return new Infrastruttura.Models.Input.InputRicercaProgetti
            {
                Cliente = input.Cliente,
                Codice = input.Codice,
                DataInizio = input.DataInizio,
                NomeProgetto = input.NomeProgetto,
                ProjectManager = input.ProjectManager,
                Tipo = input.Tipo
            };
        }
        public static Infrastruttura.Models.Input.InputRichiesteProgetti ToDto(this InputRichiesteProgetti input)
        {
            return new Infrastruttura.Models.Input.InputRichiesteProgetti
            {
                Cliente = input.Cliente,
                Codice = input.Codice,
                NomeProgetto = input.NomeProgetto,
                ProjectManager = input.ProjectManager,
                Tipo = input.Tipo,
                DataDal = input.DataDal,
                DataAl = input.DataAl,
            };
        }
    }
}
