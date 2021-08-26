using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Data.Modelli
{
    public class Progetto
    {
        public int NumProgetto { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }
        public string IdAziendale { get; set; }//chi redige il progetto
        public decimal CostoLavoro { get; set; }
        public decimal Costo { get; set; }// tipo un costo del progetto stesso (possibile preventivo)
        public string Analisi { get; set; }
        public int CodiceFornitura { get; set; }// componenti da utilizzare, posso sceglierne quanti ne voglio ?

        //da continuare dopo che capisco meglio ahaahha






    }
}
