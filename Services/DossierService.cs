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

        Task<ActionResult<IEnumerable<Dossierpatient>>> GetAll();
        Task<ActionResult<int>> Post(Dossierpatient dossierpatient);
        Task<ActionResult<Dossierpatient>> GetById(int id);
        Task<ActionResult<Dossierpatient>> GetByNumSecu(string numSecu);
        Task<ActionResult<Dossierpatient>> Delete(int id);
        Task<ActionResult<int>> Put(Dossierpatient dossierpatient);
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

        public async Task<ActionResult<Dossierpatient>> GetByNumSecu(string numsecu)
        {
            if (numsecu == "")
            {
                return null;
            }
            
            var dossierPatient = await _context.Dossierpatients
                .FirstOrDefaultAsync(m => m.NumSecu == numsecu);

            if (dossierPatient == null)
            {
                return null;
            }

            return dossierPatient;

        }

        public async Task<ActionResult<IEnumerable<Dossierpatient>>> GetAll()
        {
            var dossPatient = await _context.Dossierpatients.ToListAsync();

            return dossPatient;
        }

        public async Task<ActionResult<Dossierpatient>> Delete(int id)
        {
            var dossier = await _context.Dossierpatients.FindAsync(id);
            _context.Dossierpatients.Remove(dossier);
            await _context.SaveChangesAsync();
            return dossier;
        }
        public async Task<ActionResult<int>> Put(Dossierpatient dossierpatient)
        {
            _context.Dossierpatients.Update(dossierpatient);
            return await _context.SaveChangesAsync();
        }
    }
}
