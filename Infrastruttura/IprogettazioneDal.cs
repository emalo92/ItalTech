using Infrastruttura.Models;
using Infrastruttura.Models.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastruttura
{
    public interface IProgettazioneDal
    {
        Task<List<Progetto>> GetAllProgetti();
        Task<List<Progetto>> GetAllProgetti(InputRicercaProgetti input);
        Task<Progetto> GetProgetto(string codice);
        Task<bool> CheckProgettoExist(string codice);
        Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud);
        Task<List<RichiestaProgetto>> GetAllRichiesteProgetti();
        Task<List<RichiestaProgetto>> GetAllRichiesteProgetti(InputRicercaRichiesteProgetti input);
        Task<List<Impiegato>> GetAllImpiegati();
        Task<List<Impiegato>> GetAllImpiegati(InputRicercaImpiegati input);
        Task<List<Fornitura>> GetAllForniture();
        Task<List<Fornitura>> GetAllForniture(InputRicercaForniture input);
        Task<List<Fornitore>> GetAllFornitori();
        Task<List<Fornitore>> GetAllFornitori(InputRicercaFornitori input);
        Task<List<ProdottoCase>> GetAllProdottiCase();
        Task<List<ProdottoCase>> GetAllProdottiCase(InputRicercaProdottiCase input);
        Task<List<ProdottoAssemblato>> GetAllProdottiAssemblati();
        Task<List<ProdottoAssemblato>> GetAllProdottiAssemblati(InputRicercaProdottiAssemblati input);
        Task<List<Componente>> GetAllComponenti();
        Task<List<Componente>> GetAllComponenti(InputRicercaComponenti input);
        Task<List<Ordini>> GetAllOrdini();
        Task<List<Ordini>> GetAllOrdini(InputRicercaOrdini input);
        Task<List<OrdineForniture>> GetAllOrdiniForniture();
        Task<List<OrdineForniture>> GetAllOrdiniForniture(InputRicercaOrdiniForniture input);
        Task<List<Prototipo>> GetAllPrototipi();
        Task<List<Prototipo>> GetAllPrototipi(InputRicercaPrototipi input);
        Task<List<Cliente>> GetAllClienti();
        Task<List<Cliente>> GetAllClienti(InputRicercaClienti input);


    }
}
