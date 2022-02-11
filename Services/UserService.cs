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
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<ActionResult<IEnumerable<User>>> GetAll();
        Task<ActionResult<User>> GetById(int id);
        Task<ActionResult<int>> Post(User user);
        Task<ActionResult<int>> Delete(int id);
        Task<ActionResult<bool>> Exists(int id);
    }

    public class UserService : IUserService
    {
        private readonly FlagSHospitalContext _context = new FlagSHospitalContext();

        //private readonly FlagSHospitalContext _context;

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<User> _users = new List<User>
        //{
        //new User { Id = 1, Email = "Test@test.fr", Role = "User", Motdepasse = "test"}
        //};

        //private readonly IOptions<AppSettings> _appSettings;

        //private readonly FlagSHospitalContext _context;

        //public UserService(IOptions<AppSettings> appSettings, FlagSHospitalContext _context)
        //{
        //    _appSettings = appSettings.Value;
        //}
        public UserService()
        {
            
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            Console.WriteLine(model.Email);
            Console.WriteLine(model.Motdepasse);
            Console.WriteLine(Common.Secure.Encrypteur(model.Motdepasse));
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Motdepasse == Common.Secure.Encrypteur(model.Motdepasse));
            Console.WriteLine(user);
            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<ActionResult<int>> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task<ActionResult<bool>> Exists(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return true;
            }

            return false;

            throw new NotImplementedException();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return await _context.Users.ToListAsync();
            
        }

        public async Task<ActionResult<User>> GetById(int id)
        {

            if (id == null)
            {
                return null;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return null;
            }

            return user;
            
        }

        public async Task<ActionResult<int>> Post(User user)
        {
            User temp = new User(user.Id, user.Email, user.Role, Common.Secure.Encrypteur(user.Motdepasse));
            _context.Users.Add(temp);
            /*_context.Users.Add(user);*/
            return await _context.SaveChangesAsync();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var key = Encoding.ASCII.GetBytes("blablavlkfdqjlkvndsjkfnbsdlkbnlkdqfnfbkjnslkdvnkjdqnkbjndsfkj");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
