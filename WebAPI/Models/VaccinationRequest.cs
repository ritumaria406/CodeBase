using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class VaccinationRequest
    {   
        [Key]
        public int VaccinationId { get; set; }

        [Required]
        public string VaccinationName { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public DateTime DiscoveredOn { get; set; } 
    }
}
