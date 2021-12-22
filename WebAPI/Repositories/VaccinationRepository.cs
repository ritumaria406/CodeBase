using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using Microsoft.Data.SqlClient;

namespace WebAPI.Repositories
{
    public class VaccinationRepository : IVaccinationRepository
    {

        private readonly VaccinationContext _context;
        
        public VaccinationRepository(VaccinationContext context)
        {
            _context = context;
        }
        public async Task<VaccinationRequest> CreateVaccination(VaccinationRequest vaccine)
        {
            _context.VaccinationRequest.Add(vaccine);
            await _context.SaveChangesAsync();

            return vaccine;
        }

        public async Task DeleteVaccination(int id)
        {
            var vaccineToDelete = await _context.VaccinationRequest.FindAsync(id);
            _context.VaccinationRequest.Remove(vaccineToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VaccinationRequest>> GetVaccination()
        {
            return await _context.VaccinationRequest.ToListAsync();
        }

        public async Task<VaccinationRequest> GetVaccinationById(int vaccinationId)
        {
            return await _context.VaccinationRequest.FindAsync(vaccinationId);
        }

        public async Task UpdateVaccination(VaccinationRequest vaccine)
        {
            _context.Entry(vaccine).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }


        public async Task<CountryDetails> CreateCountry(CountryDetails country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task<IEnumerable<CountryDetails>> GetCountryDetails()
        {
            return await _context.Countries.ToListAsync();
        }


        public async Task<CountryDetails> GetCountryById(int countryId)
        {
            return await _context.Countries.FindAsync(countryId);
        }
    }
}
