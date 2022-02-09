using FlagShipHospitalBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace FlagShipHospitalBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DossierpatientController : ControllerBase
    {
        public readonly FlagSHospitalContext _context;

        public DossierpatientController(FlagSHospitalContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dossierpatient>>> GetDossierPatient()
        {
            return await _context.Dossierpatients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dossierpatient>> GetDossierPatient(int id)
        {
            var dossier = await _context.Dossierpatients.Where(d => d.Id == id).FirstOrDefaultAsync();
            if (dossier == null)
            {
                return NotFound();
            }
            return dossier;
        }

        [HttpPost]
        public async Task<ActionResult<Dossierpatient>> CreateDossierPatient(Dossierpatient dossier)
        {
            _context.Dossierpatients.Add(dossier);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDossierPatient), new { id = dossier.Id }, dossier);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dossierpatient>> DeleteDossierPatient(int id)
        {
            var dossier = await _context.Dossierpatients.FindAsync(id);
            if (dossier == null)
            {
                return NotFound();
            }
            _context.Dossierpatients.Remove(dossier);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
