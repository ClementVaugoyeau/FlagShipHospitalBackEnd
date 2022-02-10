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

        private IUserService _userService;

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
            var response = await _userService.GetAll();

            return Ok(response);
        }

        // GET: Users/Details/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);
                
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: Users/Create
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            var response = await _userService.Post(user);

            if (response == null)
                return BadRequest(new { message = "User non créé" });
            Console.WriteLine(response);
            return Ok(response);
        }

        // POST: Users/Delete/5
        ////[HttpPost, ActionName("Delete")]
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteById(int id)
        {
            var response = await _userService.Delete(id);

            if (response == null)
                return BadRequest(new { message = "User delete" });

            return Ok(response);
        }

        //private async bool UserExists(int id)
        //{
        //    return await _userService.Exists(id);
        //}
    }
}
