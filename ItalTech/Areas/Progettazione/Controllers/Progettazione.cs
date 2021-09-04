using Infrastruttura;
using ItalTech.Areas.Progettazione.Models;
using ItalTech.Mapper;
using ItalTech.Models;
using ItalTech.Utility;
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

        public Progettazione(ILogger<Progettazione> logger, IProgettazioneDal progettazioneDal)
        {
            _logger = logger;
            _progettazioneDal = progettazioneDal;
        }

        public IActionResult Start()
        {
            return View();
        }

        public IActionResult ArchivioProgetti()
        {
            var input = new InputRicercaProgetti();
            //input.DataInizio = DateTime.Now;
            return View(input);
        }        

        [HttpPost]
        public async Task<IActionResult> ArchivioProgetti(InputRicercaProgetti input)
        {
            try
            {
                var resultDal = await _progettazioneDal.GetAllProgetti(input.ToDto());
                var result = resultDal.ToModel();
                if (result == null || result.Count == 0)
                {
                    var response = new Response
                    {
                        IsSucces = false,
                        Message = result.Count == 0 ? "Non ci sono progetti corrispondenti ai parametri di ricerca" : "Impossibile caricare i progetti"
                    };
                    ViewMessage.Show(this, response);
                    return View(input);
                }
                ViewBag.ListaProgetti = result;
                return View(input);
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
                ViewMessage.Show(this, response);
                return View(input);
            }
        }

        //public async Task<IActionResult> GetAllComponenti ()
        //{

        //}

        public IActionResult RichiesteProgetto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RichiesteProgetto(InputRichiesteProgetti input)
        {
            var resultDal = await _progettazioneDal.GetAllProgetti(input.ToDto());
            var result = resultDal.ToModel();
            ViewBag.ListaRichiesteProgetti = result;
            return View(input);
        }

        public IActionResult CreaProgetto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreaProgetto(Progetto progetto)
        {
            var result = await _progettazioneDal.SaveProgetto(progetto.ToDto(), TipoCrud.insert.ToDto());
            var response = new Response
            {
                IsSucces = result,
                Message = result ? "Progetto salvato correttamente" : "Impossibile salvare il progetto"
            };
            ViewMessage.ShowLocal(this, response);
            return View(progetto);
        }


        public IActionResult ModificaProgetto()
        {
            return View();
        }
    }
}
