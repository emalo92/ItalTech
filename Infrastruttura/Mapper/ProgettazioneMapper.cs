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
                CodiceFornitore = fornitura.CodiceFornitore,
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
                CodiceFornitore = fornitura.CodiceFornitore,
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
        public static ProdottoCase ToDto(this Data.Modelli.ProdottoCase prodottoCase)
        {
            return new ProdottoCase
            {
                Codice = prodottoCase.Codice,
                CodiceProgetto = prodottoCase.CodiceProgetto,
                Nome = prodottoCase.Nome,
                Quantita = prodottoCase.Quantita,
                PesoKg = prodottoCase.PesoKg,
                Descrizione = prodottoCase.Descrizione,
                Lotto = prodottoCase.Lotto,
                Colore = prodottoCase.Colore, 
                Costo = prodottoCase.Costo                
            };
        }

        public static Data.Modelli.ProdottoCase ToEntity(this ProdottoCase prodottoCase)
        {
            return new Data.Modelli.ProdottoCase
            {
                Codice = prodottoCase.Codice,
                CodiceProgetto = prodottoCase.CodiceProgetto,
                Nome = prodottoCase.Nome,
                Quantita = prodottoCase.Quantita,
                PesoKg = prodottoCase.PesoKg,
                Descrizione = prodottoCase.Descrizione,
                Lotto = prodottoCase.Lotto,
                Colore = prodottoCase.Colore,
                Costo = prodottoCase.Costo
            };
        }
        public static List<ProdottoCase> ToDto(this List<Data.Modelli.ProdottoCase> prodottoCase)
        {
            List<ProdottoCase> prodcase = new();
            foreach (var prca in prodottoCase)
            {
                prodcase.Add(prca.ToDto());
            }
            return prodcase;
        }
        public static ProdottoAssemblato ToDto(this Data.Modelli.ProdottoAssemblato prodottoAssemblato)
        {
            return new ProdottoAssemblato
            {
                Codice = prodottoAssemblato.Codice,
                CodiceProgetto = prodottoAssemblato.CodiceProgetto,
                Nome = prodottoAssemblato.Nome,
                Quantita = prodottoAssemblato.Quantita,
                Peso = prodottoAssemblato.Peso,
                Descrizione = prodottoAssemblato.Descrizione,
                Lotto = prodottoAssemblato.Lotto,
                FasciaDiMercato = prodottoAssemblato.FasciaDiMercato,
                Costo = prodottoAssemblato.Costo
            };
        }

        public static Data.Modelli.ProdottoAssemblato ToEntity(this ProdottoAssemblato prodottoAssemblato)
        {
            return new Data.Modelli.ProdottoAssemblato
            {
                Codice = prodottoAssemblato.Codice,
                CodiceProgetto = prodottoAssemblato.CodiceProgetto,
                Nome = prodottoAssemblato.Nome,
                Quantita = prodottoAssemblato.Quantita,
                Peso = prodottoAssemblato.Peso,
                Descrizione = prodottoAssemblato.Descrizione,
                Lotto = prodottoAssemblato.Lotto,
                FasciaDiMercato = prodottoAssemblato.FasciaDiMercato,
                Costo = prodottoAssemblato.Costo
            };
        }
        public static List<ProdottoAssemblato> ToDto(this List<Data.Modelli.ProdottoAssemblato> prodottoAssemblato)
        {
            List<ProdottoAssemblato> prodasse = new();
            foreach (var pras in prodottoAssemblato)
            {
                prodasse.Add(pras.ToDto());
            }
            return prodasse;
        }
        public static Componente ToDto(this Data.Modelli.Componente componente)
        {
            return new Componente
            {
                CodiceFornitura = componente.CodiceFornitura,
                CodiceProgetto = componente.CodiceProgetto,
                NumPezzi = componente.NumPezzi
            };
        }

        public static Data.Modelli.Componente ToEntity(this Componente componente)
        {
            return new Data.Modelli.Componente
            {
                CodiceFornitura = componente.CodiceFornitura,
                CodiceProgetto = componente.CodiceProgetto,
                NumPezzi = componente.NumPezzi
            };
        }
        public static List<Componente> ToDto(this List<Data.Modelli.Componente> componente)
        {
            List<Componente> compone = new();
            foreach (var com in componente)
            {
                compone.Add(com.ToDto());
            }
            return compone;
        }
        public static Ordini ToDto(this Data.Modelli.Ordini ordine)
        {
            return new Ordini
            {
                Codice = ordine.Codice,
                DataCreazione = ordine.DataCreazione,
                DataInvio = ordine.DataInvio,
                Operatore = ordine.Operatore
            };
        }

        public static Data.Modelli.Ordini ToEntity(this Ordini ordine)
        {
            return new Data.Modelli.Ordini
            {
                Codice = ordine.Codice,
                DataCreazione = ordine.DataCreazione,
                DataInvio = ordine.DataInvio,
                Operatore = ordine.Operatore
            };
        }
        public static List<Ordini> ToDto(this List<Data.Modelli.Ordini> ordine)
        {
            List<Ordini> ordin = new();
            foreach (var ord in ordine)
            {
                ordin.Add(ord.ToDto());
            }
            return ordin;
        }
        public static OrdineForniture ToDto(this Data.Modelli.OrdineForniture ordinefornitura)
        {
            return new OrdineForniture
            {
                CodiceFornitura = ordinefornitura.CodiceFornitura,
                CodiceOrdine = ordinefornitura.CodiceOrdine,
                Quantità = ordinefornitura.Quantità
            };
        }

        public static Data.Modelli.OrdineForniture ToEntity(this OrdineForniture ordinefornitura)
        {
            return new Data.Modelli.OrdineForniture
            {
                CodiceFornitura = ordinefornitura.CodiceFornitura,
                CodiceOrdine = ordinefornitura.CodiceOrdine,
                Quantità = ordinefornitura.Quantità
            };
        }
        public static List<OrdineForniture> ToDto(this List<Data.Modelli.OrdineForniture> ordinefornitura)
        {
            List<OrdineForniture> ordinfornit = new();
            foreach (var ordinef in ordinefornitura)
            {
                ordinfornit.Add(ordinef.ToDto());
            }
            return ordinfornit;
        }
        public static Prototipo ToDto(this Data.Modelli.Prototipo prototipo)
        {
            return new Prototipo
            {
                Numero = prototipo.Numero,
                Data = prototipo.Data,
                Descrizione = prototipo.Descrizione,
                CodiceProgetto = prototipo.CodiceProgetto,
                Modifiche = prototipo.Modifiche,
                MotivoFallimento = prototipo.MotivoFallimento,
                RisultatoTest = prototipo.RisultatoTest
            };
        }

        public static Data.Modelli.Prototipo ToEntity(this Prototipo prototipo)
        {
            return new Data.Modelli.Prototipo
            {
                Numero = prototipo.Numero,
                Data = prototipo.Data,
                Descrizione = prototipo.Descrizione,
                CodiceProgetto = prototipo.CodiceProgetto,
                Modifiche = prototipo.Modifiche,
                MotivoFallimento = prototipo.MotivoFallimento,
                RisultatoTest = prototipo.RisultatoTest
            };
        }
        public static List<Prototipo> ToDto(this List<Data.Modelli.Prototipo> prototipo)
        {
            List<Prototipo> protot= new();
            foreach (var pro in prototipo)
            {
                protot.Add(pro.ToDto());
            }
            return protot;
        }
        public static Cliente ToDto(this Data.Modelli.Cliente cliente)
        {
            return new Cliente
            {
                CodFiscale = cliente.CodFiscale,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                DataDiNascita = cliente.DataDiNascita,
                Cap = cliente.Cap,
                Citta = cliente.Citta,
                Indirizzo = cliente.Indirizzo
            };
        }

        public static Data.Modelli.Cliente ToEntity(this Cliente cliente)
        {
            return new Data.Modelli.Cliente
            {
                CodFiscale = cliente.CodFiscale,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                DataDiNascita = cliente.DataDiNascita,
                Cap = cliente.Cap,
                Citta = cliente.Citta,
                Indirizzo = cliente.Indirizzo
            };
        }
        public static List<Cliente> ToDto(this List<Data.Modelli.Cliente> cliente)
        {
            List<Cliente> clien = new();
            foreach (var cli in cliente)
            {
                clien.Add(cli.ToDto());
            }
            return clien;
        }

    }
}
