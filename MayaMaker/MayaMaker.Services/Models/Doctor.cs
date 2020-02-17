using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.Models
{
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }
        public Hospital AssignedHospital { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Speciality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
