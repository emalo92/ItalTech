using Infrastruttura.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruttura.Dal
{
    public class TestingDal : ITestingDal
    {
        private ItalTechDbContext context;
        string connectionString;

        public TestingDal(ItalTechDbContext context, string connectionString)
        {
            this.context = context;
            this.connectionString = connectionString;
        }
    }
}
