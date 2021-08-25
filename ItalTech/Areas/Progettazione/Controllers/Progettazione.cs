using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItalTech.Areas.Progettazione.Controllers
{
    [Area("Progettazione")]
    public class Progettazione : Controller
    {
        public IActionResult Start()
        {
            return View();
        }

        public IActionResult ArchivioProgetti()
        {
            return View();
        }
    }
}
