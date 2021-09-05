using Infrastruttura;
using ItalTech.Areas.Progettazione.Models;
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

        public async Task<IActionResult> GetAllProgettiModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Progetti",
                ColumnNames = new List<string> { "Codice", "Data Inizio", "Data Fine", "Tipo", "Nome Progetto", "Descrizione", "Codice Analisi", "Costo Previsto", "Costo Finale", "Cliente", "ProjectManager" },
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero dei progetti",
            };
            try
            {
                var resultDal = await _progettazioneDal.GetAllProgetti();
                var result = resultDal.ToModel();
                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono progetti";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].Codice);
                    genericTable.Elements[i].Add(result[i].DataInizio);
                    genericTable.Elements[i].Add(result[i].DataFine);
                    genericTable.Elements[i].Add(result[i].Tipo);
                    genericTable.Elements[i].Add(result[i].NomeProgetto);
                    genericTable.Elements[i].Add(result[i].Descrizione);
                    genericTable.Elements[i].Add(result[i].CodiceAnalisiMercato);
                    genericTable.Elements[i].Add(result[i].CostoPrevisto);
                    genericTable.Elements[i].Add(result[i].CostoFinale);
                    genericTable.Elements[i].Add(result[i].Cliente);
                    genericTable.Elements[i].Add(result[i].ProjectManager);

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

        public async Task<IActionResult> GetProgettoAsync(string codice)
        {
            return View();
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
            try
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


        public IActionResult ModificaProgetto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModificaProgetto(Progetto progetto)
        {
            try
            {
                var result = await _progettazioneDal.SaveProgetto(progetto.ToDto(), TipoCrud.update.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Progetto salvato correttamente" : "Impossibile modificare il progetto"
                };
                ViewMessage.ShowLocal(this, response);
                return View(progetto);
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
