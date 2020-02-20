using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MayaMaker.Services.Models
{
    public class PatientKin
    {
        public Patient Patient { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string Street { get; set; } 
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string HomePhoneNumber { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
