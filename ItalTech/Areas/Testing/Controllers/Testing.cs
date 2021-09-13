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
        public IActionResult ListaTest()
        {
            return View();
        }
        public IActionResult ModificaTest()
        {
            return View();
        }

        public IActionResult EffettuaTest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreaTest(Test test)
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

        [HttpPost]
        public async Task<IActionResult> ModificaTest(Test test)
        {
            try
            {
                var result = await _testingDal.SaveTest(test.ToDto(), TipoCrud.update.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Test modificato correttamente" : "Impossibile modificare il Test"
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
        
        public async Task<IActionResult> GetAllTestModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Test",
                ColumnNames = new List<string> { "Codice", "Nome Test", "Descrizione", "Valori di Riferimento", "Quantità Eseguiti", "Quantità Passati", "Quantità Falliti", "Operatore" },
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero dei Test",
            };
            try
            {
                var resultDal = await _testingDal.GetAllTest();
                var result = resultDal.ToModel();
                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono Test";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].Codice);
                    genericTable.Elements[i].Add(result[i].Tipo);
                    genericTable.Elements[i].Add(result[i].Descrizione);
                    genericTable.Elements[i].Add(result[i].ValoriDiRiferimento);
                    genericTable.Elements[i].Add(result[i].QuantitaEseguiti);
                    genericTable.Elements[i].Add(result[i].QuantitaFalliti);
                    genericTable.Elements[i].Add(result[i].QuantitaPassati);
                    genericTable.Elements[i].Add(result[i].Operatore);
              

                }

                ViewBag.SizeModal = "modal-xl";
                return PartialView("_GenericTable", genericTable);

            }
            catch (Exception ex)
            {
                responseFailed.Message = ex.Message;
                genericTable.Elements = new List<List<object>>();
                ViewMessage.ShowLocal(this, responseFailed);
                return PartialView("_GenericTable", genericTable);
            }
        }
        public async Task<JsonResult> GetTestAsync(string codice)
        {
            try
            {
                var resultDal = await _testingDal.GetTest(codice);
                var result = resultDal.ToModel();
                var response = new Response
                {
                    IsSucces = result != null,
                    Message = result != null ? "" : "Nessun test trovato",
                    Result = result
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
                return Json(response);
            }
        }
    }
}
