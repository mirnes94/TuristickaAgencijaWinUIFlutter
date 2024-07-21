using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Gradovi;

namespace TuristickaAgencija.WebAPI.Controllers
{
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GradoviController : ControllerBase
    {
        private readonly IGradoviService _gradoviService;
        public GradoviController(IGradoviService gradoviService)
        {
            _gradoviService = gradoviService;
        }
        [HttpGet]
        public ActionResult<List<Gradovi>> Get()
        {
            return _gradoviService.Get();
        }

        [HttpGet("{id}")]
        public Model.Gradovi GetById(int id)
        {
            return _gradoviService.GetById(id);

        }
        [HttpPost]
        public Model.Gradovi Insert(GradoviInsertUpdateRequest request)
        {
            return _gradoviService.Insert(request);
        }
        [HttpPut("{id}")]
        public Gradovi Update(int id, GradoviInsertUpdateRequest request)
        {
            return _gradoviService.Update(id, request);
        }
    }
}
