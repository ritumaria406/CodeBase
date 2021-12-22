using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class VaccinationContext : DbContext
    {
        public VaccinationContext(DbContextOptions<VaccinationContext> options) :base(options)
        {            
        }

        //Ensure the DBSet variable name is same as the table name in the DB
        public DbSet<VaccinationRequest> VaccinationRequest { get; set; }

        public DbSet<CountryDetails> Countries { get; set; }
    }
}
