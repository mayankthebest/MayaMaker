using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MayaMaker.Services.Models
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Deathdate { get; set; }
        public string Ssn { get; set; }
        public string Passport { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string MaidenName { get; set; }
        public string MaritalStatus { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string Gender { get; set; }
        public string Birthplace { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
    }
}
