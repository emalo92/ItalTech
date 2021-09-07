using Infrastruttura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Testing.Controllers
{
    [Area("Testing")]
    public class Testing : Controller
    {
        private readonly ILogger<Testing> _logger;
        private ITestingDal _testingDal;

        public Testing(ILogger<Testing> logger, ITestingDal testingDal)
        {
            _logger = logger;
            _testingDal = testingDal;
        }
        public IActionResult IndexTest()
        {
            return View();
        }
    }
}
