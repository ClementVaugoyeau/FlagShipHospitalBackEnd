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

        [HttpGet("{id}")]
        public async Task<ActionResult<Dossierpatient>> GetByID(int id)
        {

            var dossier = await _dossierPatientService.GetById(id);

            if (dossier == null)
            {
                return NotFound();
            }
            return dossier;
        }
        
        
        [HttpGet("nom/{nom}")]
        public async Task<ActionResult<Dossierpatient>> GetByName(string nom)
        {

            var dossier = await _dossierPatientService.GetByName(nom);

            if (dossier == null)
            {
                return NotFound();
            }
            return dossier;
        }

       

        

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
