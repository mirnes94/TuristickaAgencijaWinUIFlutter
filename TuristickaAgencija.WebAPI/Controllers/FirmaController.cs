using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Firma;

namespace TuristickaAgencija.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FirmaController : ControllerBase
    {
        private readonly IFirmaService _firmaService;
        public FirmaController(IFirmaService firmaService)
        {
            _firmaService = firmaService;
        }
        [HttpGet]
        public ActionResult<List<Model.Firma>> Get()
        {
            return _firmaService.Get();

        }
        [HttpGet("{id}")]
        public Model.Firma GetById(int id)
        {
            return _firmaService.GetById(id);

        }

        [HttpPost]
        public Model.Firma Insert(FirmaInsertUpdateRequest request)
        {
            return _firmaService.Insert(request);
        }
        [HttpPut("{id}")]
        public Model.Firma Update(int id, FirmaInsertUpdateRequest request)
        {
            return _firmaService.Update(id, request);
        }
    }
}
