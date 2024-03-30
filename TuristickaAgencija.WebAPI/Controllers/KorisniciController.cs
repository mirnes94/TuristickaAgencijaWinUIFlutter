using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;
using TuristickaAgencija.WebAPI.Services.Korisnici;


namespace TuristickaAgencija.WebAPI.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        
        private readonly IKorisniciService _korisniciService;

        public KorisniciController( IKorisniciService korisniciService)
        {
            _korisniciService = korisniciService;
           
        }

        [HttpGet]
        public List<Model.Korisnici> Get([FromQuery]KorisniciSearchRequest request)
        {
            return _korisniciService.Get(request);

        }
       
        [HttpGet("{id}")]
        public Model.Korisnici GetById(int id)
        {
            return _korisniciService.GetById(id);

        }

        [HttpPost]
        public  Model.Korisnici Insert(KorisniciInsertUpdateRequest request)
        {
           
            return _korisniciService.Insert(request);
        }
      
        [HttpPut("{id}")]
        public Model.Korisnici Update(int id, KorisniciInsertUpdateRequest request)
        {
            return _korisniciService.Update(id, request);
        }
        [HttpGet]
        [Route("Authenticiraj/{username},{password}")]
        public Model.Korisnici Authenticiraj(string username, string password)
        {
            return _korisniciService.Authenticiraj(username, password);
        }

        [HttpGet]
        [Route("Potvrdi/{username}")]
        public Model.Korisnici Potvrdi(string username)
        {

            return _korisniciService.Potvrdi(username);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _korisniciService.Delete(id);
        }



    }
}
