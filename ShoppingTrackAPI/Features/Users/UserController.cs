using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShoppingTrackAPI.HelperFunctions;
using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Users
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
        private readonly IMediator _mediator;

        public UserController(ShoppingTrackContext context, ILogger<UserController> logger,
            IHelper helper, ICognitoUserManagement cognitoUserManagement, IConfiguration config,
            IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _helper = helper;
            _cognitoUserManagement = cognitoUserManagement;
            _config = config;
            _mediator = mediator;
        }

        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> Register([FromBody]User user)
        {
            try
            {
                var cancellationToken = _helper.GetCancellationToken(3000);
                var result = await _mediator.Send(new Register.Command(user), cancellationToken);

                if (!result.Successful)
                    return BadRequest(result.Error);

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
                var cancellationToken = _helper.GetCancellationToken(3000);
                var result = await _mediator.Send(new Login.Command(user), cancellationToken);

                if (!result.Successful)
                    return BadRequest(result.Error);

                return Ok(result.LoginResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
