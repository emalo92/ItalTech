using Infrastruttura;
using ItalTech.Areas.Testing.Models;
using ItalTech.Mapper;
using ItalTech.Models;
using ItalTech.Models.TableToExport;
using ItalTech.Utility;
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
        public IActionResult CreaTest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreaNuovoTest(Test test)
        {
            try
            {
                var result = await _testingDal.SaveTest(test.ToDto(), TipoCrud.insert.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Test salvato correttamente" : "Impossibile salvare il Test"
                };
                ViewMessage.Show(this, response);
                return View(test);
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
                ViewMessage.Show(this, response);
                return View();
            }
        }
    }
}
