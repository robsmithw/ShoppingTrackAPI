using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingTrackAPI.HelperFunctions;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShoppingTrackContext _context;
        private readonly IHelper _helper;
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _config;
        private readonly ICognitoUserManagement _cognitoUserManagement;

        public UserController(ShoppingTrackContext context, ILogger<UserController> logger,
            IHelper helper, ICognitoUserManagement cognitoUserManagement, IConfiguration config)
        {
            _context = context;
            _logger = logger;
            _helper = helper;
            _cognitoUserManagement = cognitoUserManagement;
            _config = config;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Register([FromBody]User user)
        {
            try
            {
                _logger.LogInformation("Starting Register function, request: {0}", user);
                var appClientId = _config.GetValue<string>("AWS:AppClientId");
                var userPoolId = _config.GetValue<string>("AWS:UserPoolId");
                
                if(user.Email == null)
                {
                    return BadRequest("Email is required.");
                }
                
                var emailAttribute = new AttributeType()
                {
                    Name = "email",
                    Value = user.Email
                };

                var allUsers = await GetUser();
                if(allUsers.Value.Where(x=>x.Email == user.Email).Any())
                {
                    return BadRequest("Email already in use.");
                }

                //create user in cognito
                await _cognitoUserManagement.AdminCreateUserAsync(user.Username, user.Password, userPoolId, appClientId,
                    new List<AttributeType>(){ emailAttribute });
                
                var retrieveUser = await GetUser(user.Username);
                if (retrieveUser.Result == NotFound() || retrieveUser.Value == null)
                {
                    //remove password, cognito will take care of that for us, so we don't store this
                    user.Password = string.Empty;
                    user.User_Id = await GetNextAvailableId();
                    await _context.User.AddAsync(user);
                    await _context.SaveChangesAsync();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]User user)
        {
            try
            {
                var appClientId = _config.GetValue<string>("AWS:AppClientId");
                var userPoolId = _config.GetValue<string>("AWS:UserPoolId");
                //find user in our user table for userId
                var retrievedUser = await _context.User
                    .FirstOrDefaultAsync(x => x.Username == user.Username);
                if(retrievedUser is null)
                {
                    return NotFound("User Not Found");
                }

                var loginResponse = new LoginResponse()
                {
                    UserId = retrievedUser.User_Id
                };

                var authResponse = await _cognitoUserManagement.AdminAuthenticateUserAsync(user.Username, user.Password, userPoolId, appClientId);
                if (authResponse.AuthenticationResult == null)
                    throw new Exception("No Authentication result.");
                    
                _logger.LogInformation("Authentication Result: {@result}", authResponse.AuthenticationResult);
                loginResponse.AccessToken = authResponse.AuthenticationResult.AccessToken;
                
                return Ok(loginResponse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/User/aUserName
        [HttpGet("{username}")]
        public async Task<ActionResult<User>> GetUser(string username)
        {
            _logger.LogInformation("Attempting to find user with username: {username}", username);
            var user = await _context.User.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                _logger.LogInformation("User not found.");
                return NotFound();
            }
            _logger.LogInformation("User found.");

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.User_Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { username = user.Username }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.User_Id == id);
        }

        private async Task<int> GetNextAvailableId()
        {
            return await _context.User.OrderByDescending(x => x.User_Id).Select(x => x.User_Id).FirstOrDefaultAsync() + 1;
        }
    }
}
