using Infrastruttura.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Mapper
{
   public static class TestingMapper
    {
        public static Test ToDto(this Data.Modelli.Test test)
        {
            return new Test
            {
                Codice = test.Codice,
                Tipo = test.Tipo,
                Descrizione = test.Descrizione,
                ValoriDiRiferimento = test.ValoriDiRiferimento,
                QuantitaEseguiti = test.QuantitaEseguiti,
                QuantitaFalliti = test.QuantitaFalliti,
                QuantitaPassati = test.QuantitaPassati,
                Operatore = test.Operatore, 
            };
        }

        public static Data.Modelli.Test ToEntity(this Test test)
        {
            return new Data.Modelli.Test
            {
                Codice = test.Codice,
                Tipo = test.Tipo,
                Descrizione = test.Descrizione,
                ValoriDiRiferimento = test.ValoriDiRiferimento,
                QuantitaEseguiti = test.QuantitaEseguiti,
                QuantitaFalliti = test.QuantitaFalliti,
                QuantitaPassati = test.QuantitaPassati,
                Operatore = test.Operatore,
            };
        }

        public static List<Test> ToDto(this List<Data.Modelli.Test> tests)
        {
            List<Test> tst = new();
            foreach (var test in tests)
            {
                tst.Add(test.ToDto());
            }
            return tst;
        }

        public static TestProdottoCase ToDto(this Data.Modelli.TestProdottoCase testprodcase) 
        {
            return new TestProdottoCase { 
                  CodiceProdottoCase = testprodcase.CodiceProdottoCase,
                  CodiceTest = testprodcase.CodiceTest,
            };
        }

        public static Data.Modelli.TestProdottoCase ToEntity(this TestProdottoCase testprodcase) 
        {
            return  new Data.Modelli.TestProdottoCase {
                CodiceProdottoCase = testprodcase.CodiceProdottoCase,
                CodiceTest = testprodcase.CodiceTest,
            };
        }

        public static List<TestProdottoCase> ToDto(this List<Data.Modelli.TestProdottoCase> testprodcase) 
        {
            List<TestProdottoCase> tpc = new();
            foreach(var testpc in testprodcase )
            {
                tpc.Add(testpc.ToDto());
            }
            return tpc;
        }

        public static TestProdottoAssemblato ToDto(this Data.Modelli.TestProdottoAssemblato testprodass)
        {
            return new TestProdottoAssemblato
            {
                CodiceProdottoAssemblato = testprodass.CodiceProdottoAssemblato,
                CodiceTest = testprodass.CodiceTest,
            };
        }

        public static Data.Modelli.TestProdottoAssemblato ToEntity(this TestProdottoAssemblato testprodass)
        {
            return new Data.Modelli.TestProdottoAssemblato
            {
                CodiceProdottoAssemblato = testprodass.CodiceProdottoAssemblato,
                CodiceTest = testprodass.CodiceTest,
            };
        }

        public static List<TestProdottoAssemblato> ToDto(this List<Data.Modelli.TestProdottoAssemblato> testprodass)
        {
            List<TestProdottoAssemblato> tpa = new();
            foreach (var testpa in testprodass)
            {
                tpa.Add(testpa.ToDto());
            }
            return tpa;
        }
    }
}
