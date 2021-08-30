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
    }
}
