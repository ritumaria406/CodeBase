using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class CountryDetails
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public string Description { get; set; }
    }
}
