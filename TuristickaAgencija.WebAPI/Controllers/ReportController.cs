using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model;
using TuristickaAgencija.WebAPI.Services.Uplate;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReportController : Controller
    {
        /*
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;   
        }*/
        
        private readonly IUplateService _uplateService;

        
        public ReportController(IUplateService uplateService)
        {
            _uplateService = uplateService;
        }
        // GET: ReportController
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        public ActionResult PrintUplate(int mjesec)
        {
            List<Uplate> listaUplata = _uplateService.GetByMonth(mjesec);
            /*
            List<Uplate> tempUplate = new List<Uplate>();

            for(int i=1;i<=listaUplata.Count();i++)
            {
                if (listaUplata[i].DatumUplate.Month == mjesec)
                {
                    tempUplate.Add(listaUplata[i]);
                }
                
            }*/

            UplateReport rpt = new UplateReport(_uplateService);
           return File(rpt.Report(listaUplata),"application/pdf");
           //return View();
        }
       

    }
}
