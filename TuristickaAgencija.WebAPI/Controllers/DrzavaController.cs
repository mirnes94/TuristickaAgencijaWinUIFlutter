using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Drzava;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DrzavaController : ControllerBase
    {
        private readonly IDrzavaService _drzavaService;
        public DrzavaController(IDrzavaService drzavaService)
        {
            _drzavaService = drzavaService;
        }
        [HttpGet]
        public ActionResult<List<Drzava>> Get()
        {
           return _drzavaService.Get();

        }
        [HttpGet("{id}")]
        public Model.Drzava GetById(int id)
        {
            return _drzavaService.GetById(id);
            
        }
        
        [HttpPost]
        public Model.Drzava Insert(DrzavaInsertUpdateRequest request)
        {
            return _drzavaService.Insert(request);
        }

        [HttpPut("{id}")]
        public Drzava Update(int id, DrzavaInsertUpdateRequest request)
        {
            return _drzavaService.Update(id, request);
        }
    }
}
