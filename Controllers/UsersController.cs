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
    public class UsersController : Controller
    {
        //private readonly FlagSHospitalContext _context;

        private IUserService _userService;

        //public UsersController(FlagSHospitalContext context)
        //{
        //    _context = context;
        //}

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

       // GET: Users
       [Authorize]
       [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //return await _context.Users.ToListAsync();
            var response = await _userService.GetAll();

            return Ok(response);
        }

        //// GET: Users/Details/5
        //[Authorize]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // GET: Users/Create
        //[HttpPost]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            var response = await _userService.Post(user);

            if (response == null)
                return BadRequest(new { message = "User non créé" });

            return Ok(response);
        }

        //// GET: Users/Edit/5
        //[HttpGet("/Edit/{id}")]
        //[Authorize]
        //public async Task<ActionResult<User>> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult<User>> Edit(int id, [Bind("Id,Email,Motdepasse,Role")] User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(user);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserExists(user.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return user;
        //}

        // GET: Users/Delete/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        // POST: Users/Delete/5
        ////[HttpPost, ActionName("Delete")]
        //[Authorize]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteConfirmed(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
