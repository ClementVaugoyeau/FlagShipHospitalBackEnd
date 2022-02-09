using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace FlagShipHospitalBackEnd.Models
{
    public partial class Dossierpatient
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime? DateArrivee { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime? DateDepart { get; set; }
        public string? Note { get; set; }
    }
}
