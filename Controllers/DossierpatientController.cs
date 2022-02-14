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
    [Authorize]
    public class DossierpatientController : Controller
    {
        private IDossierPatientService _dossierPatientService;

        public DossierpatientController(IDossierPatientService dossierPatientService)
        {
            _dossierPatientService = dossierPatientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dossierpatient>>> GetDossierPatient()
        {
            var dossier = await _dossierPatientService.GetAll();

            if (dossier == null)
            {
                return NotFound();
            }
            return dossier;

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dossierpatient>> Delete(int id)
        {
            var dossier = await _dossierPatientService.Delete(id);

            if (id == null)
                return BadRequest("Not a valid id");

            return Ok(dossier);
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

        [HttpGet("numsecu/{numsecu}")]
        public async Task<ActionResult<Dossierpatient>> GetDossierByNumSecu(string numsecu)
        {
 
            var dossier = await _dossierPatientService.GetByNumSecu(numsecu);

            if (dossier == null)
            {
                return NotFound();
            }
            return dossier;
        }

        [HttpPut]
        public async Task<ActionResult<Dossierpatient>> PutDossier(Dossierpatient dossier)
        {
            var d = await _dossierPatientService.Put(dossier);
            return Ok(d);
        }

    }
}
