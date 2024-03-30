using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Uplate;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UplateController : ControllerBase
    {


        private readonly IUplateService _uplateService;
        public UplateController(IUplateService uplateService)
        {
            _uplateService = uplateService;
        }
        

        [HttpGet]
        public ActionResult<List<Model.Uplate>> Get([FromQuery] UplateSearchRequest request)
        {
            return _uplateService.Get(request);

        }
        [HttpGet("GetByMonth/{mjesec}")]
        public ActionResult<List<Model.Uplate>> GetByMonth(int mjesec)
        {
            return _uplateService.GetByMonth(mjesec);

        }
       
        [HttpGet("{id}")]
        public Model.Uplate GetById(int id)
        {
            return _uplateService.GetById(id);

        }

        [HttpPost]
        public Model.Uplate Insert(UplateInsertUpdateRequest request)
        {
            return _uplateService.Insert(request);
        }
     
        [HttpPut("{id}")]
        public Model.Uplate Update(int id, UplateInsertUpdateRequest request)
        {
            return _uplateService.Update(id, request);
        }
    }
}
