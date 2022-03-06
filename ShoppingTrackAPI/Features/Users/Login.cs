using System.Threading;
using System.Threading.Tasks;

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using ShoppingTrackAPI.Models;

namespace ShoppingTrackAPI.Features.Users
{
    public class Login
    {
        public class Command : IRequest<LoginResultDto> 
        {
            public Command(User user)
            {
                User = user;
            }
            public User User { get; set; }
        }

        public class LoginResultDto 
        {
            public LoginResultDto(bool successful, string error)
            {
                Successful = successful;
                Error = error;
            }
            public LoginResponse LoginResponse { get; set; }
            public bool Successful { get; set; }
            public string Error { get; set; }
        }

        public class Handler : IRequestHandler<Command, LoginResultDto>
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
            
            public async Task<LoginResultDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var resultDto = new LoginResultDto(false, "Unknown error.");
                var user = request.User;

                var retrievedUser = await _context.User
                    .FirstOrDefaultAsync(x => x.Username == user.Username);
                if(retrievedUser is null)
                {
                    var error = "User not found.";
                    resultDto.Error = error;
                    return resultDto;
                }

                var loginResponse = new LoginResponse()
                {
                    UserId = retrievedUser.Id
                };

                var authResponse = await _cognitoUserManagement.AdminAuthenticateUserAsync(user.Username,
                    user.Password, 
                    _userPoolId, 
                    _appClientId);
                if (authResponse.AuthenticationResult == null)
                {
                    var error = "No Authentication result.";
                    resultDto.Error = error;
                    return resultDto;
                }
                    
                _logger.LogInformation("Authentication Result: {@result}", authResponse.AuthenticationResult);
                loginResponse.AccessToken = authResponse.AuthenticationResult.AccessToken;
                resultDto.Successful = true;
                resultDto.Error = string.Empty;
                resultDto.LoginResponse = loginResponse;
                
                return resultDto;
            }
        }

    }
}