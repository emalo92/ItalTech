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

    }
}
