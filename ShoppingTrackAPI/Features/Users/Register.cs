using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Users
{
    public class Register
    {
        public class Command : IRequest<RegisterResultDto> 
        {
            public Command(User user)
            {
                User = user;
            }
            public User User { get; set; }
        }

        public class RegisterResultDto 
        {
            public RegisterResultDto(bool successful, string error)
            {
                Successful = successful;
                Error = error;
            }
            public bool Successful { get; set; }
            public string Error { get; set; }
        }

        public class Handler : IRequestHandler<Command, RegisterResultDto>
        {
            private readonly ShoppingTrackContext _context;
            private readonly ILogger<Register> _logger;
            private readonly IConfiguration _config;
            private readonly ICognitoUserManagement _cognitoUserManagement;
            private readonly string _appClientId;
            private readonly string _userPoolId;
            private readonly string _appClientIdKey = "AWS:AppClientId";
            private readonly string _userPoolIdKey = "AWS:UserPoolId";

            public Handler(ShoppingTrackContext context, ILogger<Register> logger, IConfiguration config, ICognitoUserManagement cognitoUserManagement)
            {
                _context = context;
                _logger = logger;
                _config = config;
                _appClientId = _config.GetValue<string>(_appClientIdKey);
                _userPoolId = _config.GetValue<string>(_userPoolIdKey);
                _cognitoUserManagement = cognitoUserManagement;
            }
            
            public async Task<RegisterResultDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var registerResults = new RegisterResultDto(false, "Unknown error");
                var requestedUser = request.User;

                _logger.LogInformation("Starting Register function, request: {0}", requestedUser);

                if (string.IsNullOrEmpty(_appClientId) || string.IsNullOrEmpty(_userPoolId))
                {
                    var error = "Configuration is missing, please report this issue as a bug.";
                    registerResults.Error = error;
                    return registerResults;
                }
                
                if(requestedUser.Email is null)
                {
                    var error = "Email is required.";
                    registerResults.Error = error;
                    return registerResults;
                }
                
                var emailAttribute = new AttributeType()
                {
                    Name = "email",
                    Value = requestedUser.Email
                };

                if(await IsEmailInUse(requestedUser.Email, cancellationToken))
                {
                    var error = "Email already in use.";
                    registerResults.Error = error;
                    return registerResults;
                }

                //create user in cognito
                await _cognitoUserManagement.AdminCreateUserAsync(requestedUser.Username,
                    requestedUser.Password, 
                    _userPoolId, 
                    _appClientId, 
                    new List<AttributeType>(){ emailAttribute });
                
                //remove password, cognito will take care of that for us, so we don't store this
                requestedUser.Password = string.Empty;
                await _context.User.AddAsync(requestedUser, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                registerResults.Successful = true;
                registerResults.Error = string.Empty;

                return registerResults;
            }

            private async Task<bool> IsEmailInUse(string email, CancellationToken cancellationToken)
            {
                return await _context.User.AnyAsync(x => x.Email == email, cancellationToken);
            }
        }

    }
}