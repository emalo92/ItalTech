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
        Task<List<Progetto>> GetAllProgetti(InputRichiesteProgetti input);
        Task<Progetto> GetProgetto(string codice);
        Task<bool> CheckProgettoExist(string codice);
        Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud);
    }
}
