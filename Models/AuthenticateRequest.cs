using System.ComponentModel.DataAnnotations;

namespace FlagShipHospitalBackEnd.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Motdepasse { get; set; }
    }
}
