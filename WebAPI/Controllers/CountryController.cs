using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : Controller
    {

        //Vaccination Repo has details for country
        private readonly IVaccinationRepository _vaccinationRepository;

        public CountryController(IVaccinationRepository vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }


        /// <summary>
        /// By adding Action Result - it will handle not found , found ok n all that
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetCountryById")]
        public async Task<ActionResult<CountryDetails>> GetCountry(int id)
        {
            return await _vaccinationRepository.GetCountryById(id);
        }


        /// <summary>
        /// This will return the list of all the vaccines available
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<CountryDetails>> GetCountry()
        {
            try
            {
                return await _vaccinationRepository.GetCountryDetails();
            }
            catch (Exception)
            {
                return (IEnumerable<CountryDetails>)StatusCode(500);
            }
        }

        /// <summary>
        /// By adding Action Result - it will handle not found , found ok n all that
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CountryDetails>> PostCountry([FromBody] CountryDetails country)
        {
            if (ModelState.IsValid)
            {
                var newCountry = await _vaccinationRepository.CreateCountry(country);
                return CreatedAtAction(nameof(GetCountry), new { id = newCountry.CountryId }, newCountry);
            }
            return BadRequest();
        }

      
    }
}
