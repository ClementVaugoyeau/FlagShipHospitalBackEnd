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
        Task<ActionResult<Dossierpatient>> GetById(int id);
        Task<ActionResult<Dossierpatient>> GetByName(string nom);

        Task<ActionResult<int>> Update(Dossierpatient dossierpatient);
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

        public async Task<ActionResult<Dossierpatient>> GetById(int id)
        {
            if (id == null)
            {
                return null;
            }

            var dossierPatient = await _context.Dossierpatients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dossierPatient == null)
            {
                return null;
            }

            return dossierPatient;
        }

        public async Task<ActionResult<Dossierpatient>> GetByName(string nom)
        {
            if (nom == null)
            {
                return null;
            }

            var dossierPatient = await _context.Dossierpatients
                .FirstOrDefaultAsync(m => m.Nom == nom);
            if (dossierPatient == null)
            {
                return null;
            }

            return dossierPatient;
        }

        public async Task<ActionResult<int>> Update(Dossierpatient dossierpatient)
        {


            _context.Dossierpatients.Update(dossierpatient);
            return await _context.SaveChangesAsync();
        }
    }

    
}
