using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Smjestaj;

namespace TuristickaAgencija.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SmjestajController : ControllerBase
    {
        private readonly ISmjestajService _smjestajService;
        public SmjestajController(ISmjestajService smjestajService)
        {
            _smjestajService = smjestajService;
        }
        [HttpGet]
        public ActionResult<List<Model.Smjestaj>> Get()
        {
            return _smjestajService.Get();

        }
        [HttpGet("{id}")]
        public Model.Smjestaj GetById(int id)
        {
            return _smjestajService.GetById(id);

        }

        [HttpPost]
        public Model.Smjestaj Insert(SmjestajInsertUpdateRequest request)
        {
            return _smjestajService.Insert(request);
        }
        [HttpPut("{id}")]
        public Model.Smjestaj Update(int id, SmjestajInsertUpdateRequest request)
        {
            return _smjestajService.Update(id, request);
        }
    }
}
