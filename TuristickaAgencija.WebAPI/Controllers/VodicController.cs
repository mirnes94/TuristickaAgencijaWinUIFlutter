using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Vodic;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VodicController : ControllerBase
    {
        private readonly IVodicService _vodicService;
        public VodicController(IVodicService vodicService)
        {
            _vodicService = vodicService;
        }
       
        [HttpGet]
        public virtual IList<Model.Vodic> Get([FromQuery] VodicSearchRequest request)
        {
            return _vodicService.Get(request);

        }
      
        [HttpGet("{id}")]
        public Model.Vodic GetById(int id)
        {
            return _vodicService.GetById(id);

        }

        [HttpPost]
        public Model.Vodic Insert(VodicInsertUpdateRequest request)
        {
            return _vodicService.Insert(request);
        }
       
        [HttpPut("{id}")]
        public Model.Vodic Update(int id, VodicInsertUpdateRequest request)
        {
            return _vodicService.Update(id, request);
        }
    }
}
