using Infrastruttura.Models;
using Infrastruttura.Models.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura
{
    public interface ITestingDal
    {
        Task<List<Test>> GetAllTest();
        Task<List<Test>> GetAllTest(InputRicercaTest input);
        Task<List<TestProdottoAssemblato>> GetAllTestProdottiAssemblati();
        Task<List<TestProdottoAssemblato>> GetAllTestProdottiAssemblati(InputRicercaTestProdottiAssemblati input);
        Task<List<TestProdottoCase>> GetAllTestProdottiCase();
        Task<List<TestProdottoCase>> GetAllTestProdottiCase(InputRicercaTestProdottiCase input);

        Task<bool> SaveTest(Test test, TipoCrud tipoCrud);
        Task<bool> SaveTestProdottoAssemblato(int codiceTest, int codiceProdottoAssemblato, TipoCrud tipoCrud);
        Task<bool> SaveTestProdottoCase(int codiceTest, int codiceProdottoCase, TipoCrud tipoCrud);
        Task<bool> SaveTestPrototipo(int codiceTest, int codiceProgetto,int numPrototipo, TipoCrud tipoCrud);

    }
}
