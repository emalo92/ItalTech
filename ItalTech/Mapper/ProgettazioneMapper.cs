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
        public static Infrastruttura.Models.Input.InputRicercaRichiesteProgetti ToDto(this InputRicercaRichiesteProgetti input)
        {
            return new Infrastruttura.Models.Input.InputRicercaRichiesteProgetti
            {
                Cliente = input.Cliente,
                Codice = input.Codice,
                CodiceProgetto = input.CodiceProgetto,
                Operatore = input.Operatore,
                Tipo = input.Tipo,
            };
        }
        public static Infrastruttura.Models.RichiestaProgetto ToDto(this RichiestaProgetto richiestaProgetto)
        {
            return new Infrastruttura.Models.RichiestaProgetto
            {
                 Codice = richiestaProgetto.Codice,
                 CodiceProgetto = richiestaProgetto.CodiceProgetto,
                 Tipo = richiestaProgetto.Tipo,
                 Descrizione = richiestaProgetto.Descrizione,
                 Budget = richiestaProgetto.Budget,
                 Cliente = richiestaProgetto.Cliente,
                 EsitoStudio = richiestaProgetto.EsitoStudio,
                 Operatore = richiestaProgetto.Operatore
            };
        }
        public static RichiestaProgetto ToModel(this Infrastruttura.Models.RichiestaProgetto richiestaProgetto)
        {
            return new RichiestaProgetto
            {
                Codice = richiestaProgetto.Codice,
                CodiceProgetto = richiestaProgetto.CodiceProgetto,
                Tipo = richiestaProgetto.Tipo,
                Descrizione = richiestaProgetto.Descrizione,
                Budget = richiestaProgetto.Budget,
                Cliente = richiestaProgetto.Cliente,
                EsitoStudio = richiestaProgetto.EsitoStudio,
                Operatore = richiestaProgetto.Operatore
            };
        }
        public static Infrastruttura.Models.RichiestaProgetto ToModel(this RichiestaProgetto richiestaProgetto)
        {
            return new Infrastruttura.Models.RichiestaProgetto
            {
                Codice = richiestaProgetto.Codice,
                CodiceProgetto = richiestaProgetto.CodiceProgetto,
                Tipo = richiestaProgetto.Tipo,
                Descrizione = richiestaProgetto.Descrizione,
                Budget = richiestaProgetto.Budget,
                Cliente = richiestaProgetto.Cliente,
                EsitoStudio = richiestaProgetto.EsitoStudio,
                Operatore = richiestaProgetto.Operatore
            };
        }
        public static List<RichiestaProgetto> ToModel(this List<Infrastruttura.Models.RichiestaProgetto> richiestaProgettiDal)
        {
            var richpro = new List<RichiestaProgetto>();
            foreach (var rp in richiestaProgettiDal)
            {
                richpro.Add(rp.ToModel());
            }
            return richpro;
        }

        public static List<Infrastruttura.Models.RichiestaProgetto> ToModel(this List<RichiestaProgetto> richiestaProgettiDal)
        {
            var richpro = new List<Infrastruttura.Models.RichiestaProgetto>();
            foreach (var rp in richiestaProgettiDal)
            {
                richpro.Add(rp.ToModel());
            }
            return richpro;
        }
    }
}
