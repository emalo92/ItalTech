using Infrastruttura.Models;
using Infrastruttura.Models.Input;
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

        public static Data.Modelli.Progetto ToEntity(this Progetto progetto)
        {
            return new Data.Modelli.Progetto
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

        public static RichiestaProgetto ToDto(this Data.Modelli.RichiestaProgetto richiestaProgetto)
        {
            return new RichiestaProgetto
            {
                Codice = richiestaProgetto.Codice,
                CodiceProgetto = richiestaProgetto.CodiceProgetto,
                Tipo = richiestaProgetto.Tipo,
                Budget = richiestaProgetto.Budget,
                Descrizione = richiestaProgetto.Descrizione,
                EsitoStudio = richiestaProgetto.EsitoStudio,
                Cliente = richiestaProgetto.Cliente,
                Operatore = richiestaProgetto.Operatore
              };
        }

        public static Data.Modelli.RichiestaProgetto ToEntity(this RichiestaProgetto richiestaProgetto)
        {
            return new Data.Modelli.RichiestaProgetto
            {
                Codice = richiestaProgetto.Codice,
                CodiceProgetto = richiestaProgetto.CodiceProgetto,
                Tipo = richiestaProgetto.Tipo,
                Budget = richiestaProgetto.Budget,
                Descrizione = richiestaProgetto.Descrizione,
                EsitoStudio = richiestaProgetto.EsitoStudio,
                Cliente = richiestaProgetto.Cliente,
                Operatore = richiestaProgetto.Operatore
            };
        }
        public static List<RichiestaProgetto> ToDto(this List<Data.Modelli.RichiestaProgetto> richiestaProgetto) 
        {
            List<RichiestaProgetto> richprog = new();
            foreach (var rp in richiestaProgetto)
            {
                richprog.Add(rp.ToDto());
            }
            return richprog;
        }
        public static Impiegato ToDto(this Data.Modelli.Impiegato impiegato)
        {
            return new Impiegato
            { 
                CodFiscale = impiegato.CodFiscale,
                UserId = impiegato.UserId,
                Nome = impiegato.Nome,
                Cognome = impiegato.Cognome,
                DataDiNascita = impiegato.DataDiNascita,
                Citta = impiegato.Citta,
                Cap = impiegato.Cap,
                Indirizzo = impiegato.Indirizzo,
                AziendaId = impiegato.AziendaId
            };
        }

        public static Data.Modelli.Impiegato ToEntity(this Impiegato impiegato)
        {
            return new Data.Modelli.Impiegato
            {
                CodFiscale = impiegato.CodFiscale,
                UserId = impiegato.UserId,
                Nome = impiegato.Nome,
                Cognome = impiegato.Cognome,
                DataDiNascita = impiegato.DataDiNascita,
                Citta = impiegato.Citta,
                Cap = impiegato.Cap,
                Indirizzo = impiegato.Indirizzo,
                AziendaId = impiegato.AziendaId
            };
        }
        public static List<Impiegato> ToDto(this List<Data.Modelli.Impiegato> impiegato)
        {
            List<Impiegato> impie = new();
            foreach (var  imp in impiegato)
            {
                impie.Add(imp.ToDto());
            }
            return impie;
        }
        public static Fornitura ToDto(this Data.Modelli.Fornitura fornitura)
        {
            return new Fornitura
            {
                Codice = fornitura.Codice,
                Nome = fornitura.Nome,
                PartitaIva = fornitura.PartitaIva,
                CostoAlKg = fornitura.CostoAlKg,
                Tipo = fornitura.Tipo,
                SettoreDeposito = fornitura.SettoreDeposito,
                Quantita = fornitura.Quantita,
                CostoPerPezzo = fornitura.CostoPerPezzo,
                Descrizione = fornitura.Descrizione
            };
        }

        public static Data.Modelli.Fornitura ToEntity(this Fornitura fornitura)
        {
            return new Data.Modelli.Fornitura
            {
                Codice = fornitura.Codice,
                Nome = fornitura.Nome,
                PartitaIva = fornitura.PartitaIva,
                CostoAlKg = fornitura.CostoAlKg,
                Tipo = fornitura.Tipo,
                SettoreDeposito = fornitura.SettoreDeposito,
                Quantita = fornitura.Quantita,
                CostoPerPezzo = fornitura.CostoPerPezzo,
                Descrizione = fornitura.Descrizione
            };
        }
        public static List<Fornitura> ToDto(this List<Data.Modelli.Fornitura> fornitura)
        {
            List<Fornitura> fornit = new();
            foreach (var forn in fornitura)
            {
                fornit.Add(forn.ToDto());
            }
            return fornit;
        }
        public static Fornitore ToDto(this Data.Modelli.Fornitore fornitore)
        {
            return new Fornitore
            {
                PartitaIva = fornitore.PartitaIva,
                Nome = fornitore.Nome,
                Indirizzo = fornitore.Indirizzo,
                NumTelefono = fornitore.NumTelefono,
                Cap = fornitore.Cap,
                Città = fornitore.Città,
                Email = fornitore.Email,
                RagioneSociale = fornitore.RagioneSociale
            };
        }

        public static Data.Modelli.Fornitore ToEntity(this Fornitore fornitore)
        {
            return new Data.Modelli.Fornitore
            {
                PartitaIva = fornitore.PartitaIva,
                Nome = fornitore.Nome,
                Indirizzo = fornitore.Indirizzo,
                NumTelefono = fornitore.NumTelefono,
                Cap = fornitore.Cap,
                Città = fornitore.Città,
                Email = fornitore.Email,
                RagioneSociale = fornitore.RagioneSociale
            };
        }
        public static List<Fornitore> ToDto(this List<Data.Modelli.Fornitore> fornitore)
        {
            List<Fornitore> fornito = new();
            foreach (var fornitor in fornitore)
            {
                fornito.Add(fornitor.ToDto());
            }
            return fornito;
        }
    }
}
