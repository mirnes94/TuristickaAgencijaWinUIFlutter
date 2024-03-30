using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Services.Komentar;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KomentarController : ControllerBase
    {
        private readonly IKomentarService _komentarService;
        public KomentarController(IKomentarService komentarService)
        {
            _komentarService = komentarService;
        }
     
        [HttpGet]
        public ActionResult<List<Model.Komentar>> Get([FromQuery] KomentarSearchRequest request)
        {
            return _komentarService.Get(request);

        }
       
        [HttpGet("{id}")]
        public Model.Komentar GetById(int id)
        {
            return _komentarService.GetById(id);

        }

        [HttpPost]
        public Model.Komentar Insert(KomentarInsertUpdateRequest request)
        {
            return _komentarService.Insert(request);
        }
       
        [HttpPut("{id}")]
        public Model.Komentar Update(int id, KomentarInsertUpdateRequest request)
        {
            return _komentarService.Update(id, request);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _komentarService.Delete(id);
        }
    }
}
