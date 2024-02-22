using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P1.Models;
using P1;
using System.Numerics;


namespace P1.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private Mains mains;
        public HomeController()
        {
            mains = new Mains();
        }

        [HttpGet()]
        [Route("GetFilm/{id}")]
        public Films GetFilm(int id)
        {
            return Mains.GetSingle<Films>("films/" + id);
        }


        [HttpGet()]
        [Route("GetAllFilms/")]
        public ModelResult<Films> GetAllFilms(string pageNumber = "1")
        {
            ModelResult<Films> result = mains.GetAllPaginated<Films>("/films/", pageNumber);

            return result;
        }

        [HttpGet()]
        [Route("GetPeople/{id}")]
        public People GetPeople(int id)
        {
            return Mains.GetSingle<People>($"people/{id}/");
        }


        [HttpGet()]
        [Route("GetAllPeople/")]
        public ModelResult<People> GetAllPeople(string pageNumber = "1")
        {
            ModelResult<People> result = mains.GetAllPaginated<People>("/people/", pageNumber);

            return result;
        }

        [HttpGet()]
        [Route("GetPlanet/{id}")]
        public Planets GetPlanet(int id)
        {
            return Mains.GetSingle<Planets>("planets/" + id);
        }

        [HttpGet()]
        [Route("GetAllPlanets/")]
        public ModelResult<Planets> GetAllPlanets(string pageNumber = "1")
        {
            ModelResult<Planets> result = mains.GetAllPaginated<Planets>("/planets/", pageNumber);
            return result;
        }

        [HttpGet()]
        [Route("GetSpecie/{id}")]
        public Species GetSpecie(int id)
        {
            return Mains.GetSingle<Species>("species/" + id);
        }

        [HttpGet()]
        [Route("GetAllSpecies/")]
        public ModelResult<Species> GetAllSpecies(string pageNumber = "1")
        {
            ModelResult<Species> result = mains.GetAllPaginated<Species>("/species/", pageNumber);

            return result;
        }

        [HttpGet()]
        [Route("GetStarship/{id}")]
        public Starships GetStarship(int id)
        {
            return Mains.GetSingle<Starships>("starships/" + id);
        }

        [HttpGet()]
        [Route("GetAllStarships/")]
        public ModelResult<Starships> GetAllStarships(string pageNumber = "1")
        {
            ModelResult<Starships> result = mains.GetAllPaginated<Starships>("/starships/", pageNumber);

            return result;
        }

        [HttpGet()]
        [Route("GetVehicle/{id}")]
        public Vehicles GetVehicle(int id)
        {
            return Mains.GetSingle<Vehicles>("vehicles/" + id);
        }

        [HttpGet()]
        [Route("GetAllVehicles/")]
        public ModelResult<Vehicles> GetAllVehicles(string pageNumber = "1")
        {
            ModelResult<Vehicles> result = mains.GetAllPaginated<Vehicles>("/vehicles/", pageNumber);

            return result;
        }
    }
}