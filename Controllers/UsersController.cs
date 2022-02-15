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

        [Authorize("Staff", "Docteur")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var response = await _userService.GetAll();

            return Ok(response);
        }

        [Authorize("Staff", "Docteur")]
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

        [HttpPost]
        [Authorize("Docteur")]
        public async Task<ActionResult<User>> Create(User user)
        {
            var response = await _userService.Post(user);

            if (response == null)
                return BadRequest(new { message = "User non créé" });
            
            return Ok(response);
        }

        [Authorize("Docteur")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteById(int id)
        {
            var response = await _userService.Delete(id);

            if (response == null)
                return BadRequest(new { message = "User delete" });

            return Ok(response);
        }

        [HttpPut]
        [Authorize("Staff", "Docteur")]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            var u = await _userService.Put(user);
            return Ok(u);
        }

    }
}
