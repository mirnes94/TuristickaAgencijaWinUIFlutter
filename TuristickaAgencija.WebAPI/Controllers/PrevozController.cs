using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Prevoz;

namespace TuristickaAgencija.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrevozController : ControllerBase
    {
        private readonly IPrevozService _prevozService;
        public PrevozController(IPrevozService prevozService)
        {
            _prevozService = prevozService;
        }
        [HttpGet]
        public ActionResult<List<Model.Prevoz>> Get()
        {
            return _prevozService.Get();

        }
        [HttpGet("{id}")]
        public Model.Prevoz GetById(int id)
        {
            return _prevozService.GetById(id);

        }

        [HttpPost]
        public Model.Prevoz Insert(PrevozInsertUpdateRequest request)
        {
            return _prevozService.Insert(request);
        }
        [HttpPut("{id}")]
        public Model.Prevoz Update(int id, PrevozInsertUpdateRequest request)
        {
            return _prevozService.Update(id, request);
        }
    }
}
