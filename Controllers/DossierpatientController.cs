using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlagShipHospitalBackEnd.Models;
using FlagShipHospitalBackEnd.Helpers;
using FlagShipHospitalBackEnd.Services;


namespace FlagShipHospitalBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DossierpatientController : Controller
    {
        //public readonly FlagSHospitalContext _context;

        private IDossierPatientService _dossierPatientService;

        public DossierpatientController(IDossierPatientService dossierPatientService)
        {
            _dossierPatientService = dossierPatientService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(Dossierpatient dossierpatient)
        {
            var response = await _dossierPatientService.Post(dossierpatient);

            if (response == null)
                return BadRequest(new { message = "Dossierpatient non créé" });

            return Ok(response);
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Dossierpatient>>> GetDossierPatient()
        //{
        //    return await _context.Dossierpatients.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Dossierpatient>> GetDossierPatient(int id)
        //{
        //    var dossier = await _context.Dossierpatients.Where(d => d.Id == id).FirstOrDefaultAsync();
        //    if (dossier == null)
        //    {
        //        return NotFound();
        //    }
        //    return dossier;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Dossierpatient>> CreateDossierPatient(Dossierpatient dossier)
        //{
        //    _context.Dossierpatients.Add(dossier);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetDossierPatient), new { id = dossier.Id }, dossier);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Dossierpatient>> DeleteDossierPatient(int id)
        //{
        //    var dossier = await _context.Dossierpatients.FindAsync(id);
        //    if (dossier == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Dossierpatients.Remove(dossier);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}
    }
}
