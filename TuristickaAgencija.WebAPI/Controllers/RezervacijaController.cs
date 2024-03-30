using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Rezervacija;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RezervacijaController : ControllerBase
    {
        private readonly IRezervacijaService _rezervacijaService;
        public RezervacijaController(IRezervacijaService rezervacijaService)
        {
            _rezervacijaService = rezervacijaService;
        }
      
        [HttpGet]
        public ActionResult<List<Model.Rezervacija>> Get([FromQuery] RezervacijaSearchRequest request)
        {
            return _rezervacijaService.Get(request);

        }
      
        [HttpGet("{id}")]
        public Model.Rezervacija GetById(int id)
        {
            return _rezervacijaService.GetById(id);

        }
       
        [HttpPost]
        public Model.Rezervacija Insert(RezervacijaInsertUpdateRequest request)
        {
            return _rezervacijaService.Insert(request);
        }
       
        [HttpPut("{id}")]
        public Model.Rezervacija Update(int id, RezervacijaInsertUpdateRequest request)
        {
            return _rezervacijaService.Update(id, request);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _rezervacijaService.Delete(id);
        }
    }
}
