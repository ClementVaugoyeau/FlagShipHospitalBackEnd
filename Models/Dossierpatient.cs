using System;
using System.Collections.Generic;

namespace FlagShipHospitalBackEnd.Models
{
    public partial class Dossierpatient
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateTime? DateArrivee { get; set; }
        public DateTime? DateDepart { get; set; }
        public string? Note { get; set; }
    }
}
