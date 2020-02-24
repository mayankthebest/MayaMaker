using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MayaMaker.Services.Models
{
    public class Encounter
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Patient Patient { get; set; }
        public Doctor AssignedDoctor { get; set; }
        public string EncounterClass { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonDescription { get; set; }
    }
}
