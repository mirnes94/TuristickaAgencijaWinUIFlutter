using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Uloge;

namespace TuristickaAgencija.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UlogeController : ControllerBase
    {
        private readonly IUlogeService _ulogeService;
        public UlogeController(IUlogeService ulogeService)
        {
            _ulogeService = ulogeService;
        }
        [HttpGet]
        public ActionResult<List<Model.Uloge>> Get()
        {
            return _ulogeService.Get();

        }
        [HttpGet("{id}")]
        public Model.Uloge GetById(int id)
        {
            return _ulogeService.GetById(id);

        }

        [HttpPost]
        public Model.Uloge Insert(UlogeInsertUpdateRequest request)
        {
            return _ulogeService.Insert(request);
        }
        [HttpPut("{id}")]
        public Model.Uloge Update(int id, UlogeInsertUpdateRequest request)
        {
            return _ulogeService.Update(id, request);
        }
        
    }
}
