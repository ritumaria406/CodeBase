using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IVaccinationRepository
    {
        Task<IEnumerable<VaccinationRequest>> GetVaccination();

        Task<VaccinationRequest> GetVaccinationById(int id);

        Task<VaccinationRequest> CreateVaccination(VaccinationRequest vaccine);

        Task UpdateVaccination(VaccinationRequest vaccine);

        Task DeleteVaccination(int id);

        Task<IEnumerable<CountryDetails>> GetCountryDetails();

        Task<CountryDetails> CreateCountry(CountryDetails country);

        Task<CountryDetails> GetCountryById(int countryId);
    }
}
