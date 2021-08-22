using Infrastruttura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Dal
{
    public class ProgettazioneDal : IProgettazioneDal
    {
        
        public ProgettazioneDal ()
        {
            //context Todoasasd
        }

        public Task<bool> CheckProgettoExist(string codice)
        {
            throw new NotImplementedException();
        }

        public Task<List<Progetto>> GetAllProgetti()
        {
            throw new NotImplementedException();
        }

        public Task<Progetto> GetProgetto(string codice)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveProgetto(Progetto progetto, TipoCrud tipoCrud)
        {
            switch (tipoCrud)
            {
                case TipoCrud.insert: break;

            }

            throw new NotImplementedException();
        }
    }
}
