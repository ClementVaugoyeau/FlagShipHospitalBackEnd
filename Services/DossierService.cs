using FlagShipHospitalBackEnd.Helpers;
using FlagShipHospitalBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlagShipHospitalBackEnd.Services
{
    public interface IDossierPatientService
    {
        
        //Task<ActionResult<IEnumerable<User>>> GetAll();
        //Task<ActionResult<User>> GetById(int id);
        Task<ActionResult<int>> Post(Dossierpatient dossierpatient);

        //Task<ActionResult<int>> Delete(int id);

        //Task<ActionResult<bool>> Exists(int id);
    }

    public class DossierService : IDossierPatientService
    {
        private readonly FlagSHospitalContext _context = new FlagSHospitalContext();

        public DossierService()
        {

        }

        public async Task<ActionResult<int>> Post(Dossierpatient dossierpatient)
        {
            _context.Dossierpatients.Add(dossierpatient);
            return await _context.SaveChangesAsync();
        }

    }
}
