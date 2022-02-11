using FlagShipHospitalBackEnd.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlagShipHospitalBackEnd.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userService, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var key = "blablavlkfdqjlkvndsjkfnbsdlkbnlkdqfnfbkjnslkdvnkjdqnkbjndsfkj";
                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = securityKey,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };

                if (ValidateToken(token, tokenValidationParameters))
                {
                    var TokenInfo = new Dictionary<string, string>();
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token);
                    var claims = jwtSecurityToken.Claims.ToList();

                    foreach (var claim in claims)
                    {
                        TokenInfo.Add(claim.Type, claim.Value);
                    }

                    //string sub = jwtSecurityToken.Subject;
                    //string iss = jwtSecurityToken.Issuer;
                    //DateTime iat = jwtSecurityToken.IssuedAt;
                    //List<string> audiences = new List<string>(jwtSecurityToken.Audiences);
                    //DateTime exp = jwtSecurityToken.ValidTo;
                    //string bar;
                    //bool ifBar = TokenInfo.TryGetValue("foo", out bar);
                    //Console.WriteLine("Subject: " + sub);
                    //Console.WriteLine("Issuer: " + iss);
                    //Console.WriteLine("Issued At: " + iat);
                    //foreach (var member in audiences)
                    //{
                    //    Console.WriteLine("Audience: " + member);
                    //}
                    //Console.WriteLine("Expiration: " + exp);
                    //Console.WriteLine("foo: " + bar);
                    string userID;
                    string idUser = TokenInfo["id"];
                    context.Items["User"] = userService.GetById(Int32.Parse(idUser));
                }
                //var tokenHandler = new JwtSecurityTokenHandler();

                //var key = Encoding.ASCII.GetBytes("blablavlkfdqjlkvndsjkfnbsdlkbnlkdqfnfbkjnslkdvnkjdqnkbjndsfkj");
                //Console.WriteLine(key);

                //tokenHandler.ValidateToken(token, new TokenValidationParameters
                //{
                //    ValidateIssuerSigningKey = true,
                //    IssuerSigningKey = new SymmetricSecurityKey(key),
                //    ValidateIssuer = false,
                //    ValidateAudience = false,
                //    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                //    ClockSkew = TimeSpan.Zero
                //}, out SecurityToken validatedToken);

                //Console.WriteLine("validatedToken");
                //Console.WriteLine(validatedToken);
                //var jwtToken = (JwtSecurityToken)validatedToken;
                //Console.WriteLine("jwtToken");
                //Console.WriteLine(jwtToken);
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                //Console.WriteLine("userId");
                //Console.WriteLine(userId);

                //// attach user to context on successful jwt validation
                //Console.WriteLine("6");
                //context.Items["User"] = userService.GetById(6);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }

        private static bool ValidateToken(string token, TokenValidationParameters tvp)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                ClaimsPrincipal principal = handler.ValidateToken(token, tvp, out securityToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
