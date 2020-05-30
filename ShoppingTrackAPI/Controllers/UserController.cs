using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingTrackAPI.HelperFunctions;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ShoppingTrackContext _context;
        private readonly Helper _helper;

        public UserController(ShoppingTrackContext context)
        {
            _context = context;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Register([FromBody]User user)
        {
            try
            {
                if(user.Email == null)
                {
                    return BadRequest("Email is required.");
                }
                var allUsers = await GetUser();
                if(allUsers.Value.Where(x=>x.Email == user.Email).Any())
                {
                    return BadRequest("Email already in use.");
                }
                var retrieveUser = await GetUser(user.Username);
                if (retrieveUser.Result == NotFound() || retrieveUser.Value == null)
                {
                    using (SHA512 shaM = new SHA512Managed())
                    {
                        var passwordInBytes = Encoding.UTF8.GetBytes(user.Password);
                        var hash = shaM.ComputeHash(passwordInBytes);
                        if(hash != null)
                        {
                            var hashPass = Encoding.Default.GetString(hash);
                            user.Password = hashPass;
                        }
                        else
                        {
                            return BadRequest("Hash could not be generated.");
                        }
                    }
                    _context.User.Add(user);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetUser", new { id = user.User_Id }, user);
                }
                return BadRequest("User Already exist.");

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Login([FromBody]User user)
        {
            try
            {
                var retrieveUser = await GetUser(user.Username);
                if(retrieveUser.Result == NotFound() || retrieveUser.Value == null)
                {
                    return NotFound();
                }
                if(Helper.SignIn(retrieveUser.Value, user))
                {
                    return CreatedAtAction("GetUser", new { username = retrieveUser.Value.Username }, retrieveUser.Value);
                }
                else
                {
                    return NotFound();
                }

            }
            catch(Exception ex)
            {
                return BadRequest();
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
            var user = await _context.User.Where(x => x.Username == username).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

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
    }
}
