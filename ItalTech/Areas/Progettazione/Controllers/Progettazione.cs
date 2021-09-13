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
    //[Authorize]
    public class Progettazione : Controller
    {
        private readonly ILogger<Progettazione> _logger;
        private IProgettazioneDal _progettazioneDal;

        public Progettazione(ILogger<Progettazione> logger, IProgettazioneDal progettazioneDal)
        {
            _logger = logger;
            _progettazioneDal = progettazioneDal;
        }

        public async Task<IActionResult> Start()
        {
            var listaProgetti = await _progettazioneDal.GetAllProgetti();
            ViewBag.Count = listaProgetti.Count;
            ViewBag.ListaProgetti = listaProgetti.Take(5).ToList().ToModel();
            return View();
        }
        public IActionResult ModificaRichiestaProgetto()
        {
            return View();
        }
        public IActionResult CreaRichiestaProgetto()
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

        public async Task<JsonResult> GetProgettoAsync(string codice)
        {
            try
            {
                var resultDal = await _progettazioneDal.GetProgetto(codice);
                var result = resultDal.ToModel();
                var response = new Response
                {
                    IsSucces = result != null,
                    Message = result != null ? "" : "Nessun progetto trovato",
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

        public async Task<IActionResult> GetAllImpiegatiModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Impiegati",
                ColumnNames = new List<string> { "AziendaID", "Nome", "Cognome" },
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero degli impiegati",
            };
            try
            {
                var result = await _progettazioneDal.GetAllImpiegati();
                
                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono impiegati";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].AziendaId);
                    genericTable.Elements[i].Add(result[i].Nome);
                    genericTable.Elements[i].Add(result[i].Cognome);
                }

                //ViewBag.SizeModal = "modal-xl";
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

        public async Task<IActionResult> GetAllClientiModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Clienti",
                ColumnNames = new List<string> { "Codice Fiscale", "Nome", "Cognome"},
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero dei clienti",
            };
            try
            {
                var result = await _progettazioneDal.GetAllClienti();

                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono clienti";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].CodFiscale);
                    genericTable.Elements[i].Add(result[i].Nome);
                    genericTable.Elements[i].Add(result[i].Cognome);
                }

                //ViewBag.SizeModal = "modal-xl";
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

        public async Task<IActionResult> GetComponentiProgettoAsync(int codice)
        { 
            if (codice == 0)
            {
                return PartialView("_FormModificaComponenti");
            }

            var responseFailed = new Response { IsSucces = false };
            
            var componenti = await _progettazioneDal.GetAllComponenti(codice);

            if (componenti.Count == 0)
            {
                responseFailed.Message = "Non ci sono componenti per il progetto selezionato";                
                return PartialView("_FormModificaComponenti");
            }

            return PartialView("_FormModificaComponenti", componenti);
        }

        public async Task<IActionResult> GetAllFornitureModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Forniture",
                ColumnNames = new List<string> { "Codice Fornitura", "Nome", "Descrizione", "Fornitore" },
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero delle forniture",
            };
            try
            {
                var result = await _progettazioneDal.GetAllForniture();

                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono forniture";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].Codice);
                    genericTable.Elements[i].Add(result[i].Nome);
                    genericTable.Elements[i].Add(result[i].Descrizione);
                    genericTable.Elements[i].Add(result[i].CodiceFornitore);
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

        public IActionResult RichiesteProgetto()
        {
            return View();
        }
        public async Task<IActionResult> ArchivioRichiesteProgetti(InputRicercaRichiesteProgetti input)
        {
            try
            {
                var resultDal = await _progettazioneDal.GetAllRichiesteProgetti(input.ToDto());
                var result = resultDal.ToModel();
                if (result == null || result.Count == 0)
                {
                    var response = new Response
                    {
                        IsSucces = false,
                        Message = result.Count == 0 ? "Non ci sono Richieste progetti corrispondenti ai parametri di ricerca" : "Impossibile caricare i progetti"
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

        public async Task<IActionResult> GetAllRichiesteProgettiModalAsync()
        {
            var genericTable = new Table()
            {
                Title = "Elenco Rchieste Progetti",
                ColumnNames = new List<string> { "Codice", "CodiceProgetto", "Tipo", "Descrizione", "EsitoStudio", "Budget", "Operatore", "Cliente" },
                Elements = new List<List<object>>()
            };
            var responseFailed = new Response
            {
                IsSucces = false,
                Message = "Si è verificato un errore durante il recupero delle richieste progetti",
            };
            try
            {
                var resultDal = await _progettazioneDal.GetAllRichiesteProgetti();
                var result = resultDal.ToModel();
                if (result == null)
                {
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                if (result.Count == 0)
                {
                    responseFailed.Message = "Non ci sono Rchieste progetti";
                    ViewMessage.ShowLocal(this, responseFailed);
                    return PartialView("_GenericTable", genericTable);
                }
                for (var i = 0; i < result.Count; ++i)
                {
                    genericTable.Elements.Add(new List<object>());
                    genericTable.Elements[i].Add(result[i].Codice);
                    genericTable.Elements[i].Add(result[i].CodiceProgetto);
                    genericTable.Elements[i].Add(result[i].Tipo);
                    genericTable.Elements[i].Add(result[i].Descrizione);
                    genericTable.Elements[i].Add(result[i].EsitoStudio);
                    genericTable.Elements[i].Add(result[i].Budget);
                    genericTable.Elements[i].Add(result[i].Operatore);
                    genericTable.Elements[i].Add(result[i].Cliente);
                    

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
        public async Task<JsonResult> GetRichiestaProgettoAsync(string codice)
        {
            try
            {
                var resultDal = await  _progettazioneDal.GetRichiestaProgetto(codice);
                var result = resultDal.ToModel();
                var response = new Response
                {
                    IsSucces = result != null,
                    Message = result != null ? "" : "Nessun richiesta progetto trovato",
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

        [HttpPost]
        public async Task<IActionResult> RichiesteProgetto(InputRicercaRichiesteProgetti input)
        {
            var resultDal = await _progettazioneDal.GetAllRichiesteProgetti(input.ToDto());
            //var result = resultDal.ToModel();
            //ViewBag.ListaRichiesteProgetti = result;
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
                ViewMessage.Show(this, response);
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
                ViewMessage.Show(this, response);
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

        [HttpPost]
        public async Task<IActionResult> SalvaProgetto([FromBody]InputProgettoConComponenti input, TipoCrud tipoCrud)
        {
            try
            {
                var result = await _progettazioneDal.SaveProgetto(input.Testata, tipoCrud.ToDto());
                var resultComponenti = await _progettazioneDal.SaveComponenti(input.Dettaglio, tipoCrud.ToDto());

                var response = new Response
                {
                    IsSucces = result && resultComponenti,
                    Message = result && resultComponenti ? "Progetto salvato correttamente" : "Impossibile salvare il progetto"
                };
                ViewMessage.Show(this, response);
                return new JsonResult(response);
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
        public async Task<IActionResult> EliminaProgetto(Progetto progetto)
        {
            try
            {
                var result = await _progettazioneDal.SaveProgetto(progetto.ToDto(), TipoCrud.delete.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Progetto eliminato correttamente" : "Impossibile eliminare il progetto"
                };
                ViewMessage.Show(this, response);
                return View("ModificaProgetto");
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
                ViewMessage.Show(this, response);
                return View("ModificaProgetto");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreaRichiestaProgetto(RichiestaProgetto richiestaprogetto)
        {
             
             try
             {
                    var result = await _progettazioneDal.SaveRichiestaProgetto(richiestaprogetto.ToDto(), TipoCrud.insert.ToDto());
                    var response = new Response
                    {
                        IsSucces = result,
                        Message = result ? "Richiesta Progetto salvata correttamente" : "Impossibile salvare la Richiesta Progetto"
                    };
                    ViewMessage.Show(this, response);
                    return View(richiestaprogetto);
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
        public async Task<IActionResult> ModificaRichiestaProgetto(RichiestaProgetto richiestaProgetto)
        {
            try
            {
                var result = await _progettazioneDal.SaveRichiestaProgetto(richiestaProgetto.ToDto(), TipoCrud.update.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Richiesta progetto salvata correttamente" : "Impossibile modificare la Richiesta Progetto"
                };
                ViewMessage.Show(this, response);
                return View(richiestaProgetto);
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
        public async Task<IActionResult> EliminaRichiestaProgetto(RichiestaProgetto richiestaProgetto)
        {
            try
            {
                var result = await _progettazioneDal.SaveRichiestaProgetto(richiestaProgetto.ToDto(), TipoCrud.delete.ToDto());
                var response = new Response
                {
                    IsSucces = result,
                    Message = result ? "Richiesta eliminata correttamente" : "Impossibile eliminare la richiesta"
                };
                ViewMessage.Show(this, response);
                return View("ModificaRichiestaProgetto");
            }
            catch (Exception ex)
            {
                var response = new Response
                {
                    IsSucces = false,
                    Message = ex.Message
                };
                ViewMessage.Show(this, response);
                return View("ModificaRichiestaProgetto");
            }
        }

        public async Task<JsonResult> GetTestAsync(string codice)
        {
            try
            {
                var resultDal = await _progettazioneDal.GetRichiestaProgetto(codice);
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
