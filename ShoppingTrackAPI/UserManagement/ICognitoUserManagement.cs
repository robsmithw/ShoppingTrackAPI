using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon.CognitoIdentityProvider.Model;

public interface ICognitoUserManagement 
{
    Task AdminCreateUserAsync(
        string username,
        string password,
        string userPoolId,
        string appClientId,
        List<AttributeType> attributeTypes);
    Task AdminAddUserToGroupAsync(
        string username,
        string userPoolId,
        string groupName);
    Task<AdminInitiateAuthResponse> AdminAuthenticateUserAsync(
        string username,
        string password,
        string userPoolId,
        string appClientId);
    Task AdminRemoveUserFromGroupAsync(
        string username,
        string userPoolId,
        string groupName);
    Task AdminDisableUserAsync(
        string username,
        string userPoolId);
    Task AdminDeleteUserAsync(
        string username,
        string userPoolId);
}