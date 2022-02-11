using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FlagShipHospitalBackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Motdepasse { get; set; }

        public User(int id, string email, string role, string motdepasse)
        {
            Id = id;
            Email = email;
            Role = role;
            Motdepasse = motdepasse;
        }
    }

    
}
