using Infrastruttura;
using Infrastruttura.Dal;
using ItalTech.Areas.Progettazione.Models;
using ItalTech.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Controllers
{
    [Area("Progettazione")]
    public class Progettazione : Controller
    {
        private readonly ILogger<Progettazione> _logger;
        private IProgettazioneDal _progettazioneDal;

        public Progettazione (ILogger<Progettazione> logger, IProgettazioneDal progettazioneDal)
        {
            _logger = logger;
            _progettazioneDal = progettazioneDal;
        }

        public IActionResult Start()
        {
            return View();
        }

        public async Task<IActionResult> ArchivioProgetti()
        {
            var input = new InputRicercaProgetti();
            input.DataInizio = DateTime.Now;
            return View(input);
        }

        [HttpPost]
        public async Task<IActionResult> ArchivioProgetti(InputRicercaProgetti input)
        {
            var resultDal = await _progettazioneDal.GetAllProgetti(input.ToDto());
            var result = resultDal.ToModel();
            ViewBag.ListaProgetti = result;
            return View(input);
        }


    }
}
