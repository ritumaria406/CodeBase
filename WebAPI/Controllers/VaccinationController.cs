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
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationRepository _vaccinationRepository;

        public VaccinationController(IVaccinationRepository vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }       

        /// <summary>
        /// By adding Action Result - it will handle not found , found ok n all that
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetVaccinationById")]
        public async Task<ActionResult<VaccinationRequest>> GetVaccination(int id)
        {
            return await _vaccinationRepository.GetVaccinationById(id);
        }

        /// <summary>
        /// This will return the list of all the vaccines available
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<VaccinationRequest>> GetVaccination()
        {
            try
            {
                return await _vaccinationRepository.GetVaccination();
            }
            catch(Exception)
            {
                return (IEnumerable<VaccinationRequest>)StatusCode(500);
            }
        }       

        /// <summary>
        /// By adding Action Result - it will handle not found , found ok n all that
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<VaccinationRequest>> PostVaccination([FromBody] VaccinationRequest vaccine)
        {
            if (ModelState.IsValid) {
                var newVaccine = await _vaccinationRepository.CreateVaccination(vaccine);
                return CreatedAtAction(nameof(GetVaccination), new { id = newVaccine.VaccinationId }, newVaccine);
            }
            return BadRequest();
        }

        /// <summary>
        /// By adding Action Result - it will handle not found , found ok n all that
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<VaccinationRequest>> PutVaccination(int id, [FromBody] VaccinationRequest vaccine)
        {
            if(!id.Equals(vaccine.VaccinationId))
            {
                return BadRequest();
            }

            await _vaccinationRepository.UpdateVaccination(vaccine);
            return NoContent();
        }
    }
}
