using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

public class CognitoUserManagement : ICognitoUserManagement
{
    private readonly AWSCredentials awsCredentials;
    private readonly AmazonCognitoIdentityProviderClient adminAmazonCognitoIdentityProviderClient;
    private readonly AmazonCognitoIdentityProviderClient anonymousAmazonCognitoIdentityProviderClient;

    public CognitoUserManagement(string profileName = "default")
    {
        RegionEndpoint regionEndpoint = RegionEndpoint.USWest1;
        CredentialProfileStoreChain credentialProfileStoreChain = new CredentialProfileStoreChain();

        if (credentialProfileStoreChain.TryGetAWSCredentials(profileName, out AWSCredentials internalAwsCredentials))
        {
            awsCredentials = internalAwsCredentials;
            adminAmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(
                awsCredentials,
                regionEndpoint);
            anonymousAmazonCognitoIdentityProviderClient = new AmazonCognitoIdentityProviderClient(
                new AnonymousAWSCredentials(),
                regionEndpoint);
        }
        else
        {
            throw new ArgumentNullException(nameof(AWSCredentials));
        }
    }

    public async Task AdminCreateUserAsync(
        string username,
        string password,
        string userPoolId,
        string appClientId,
        List<AttributeType> attributeTypes)
    {
        AdminCreateUserRequest adminCreateUserRequest = new AdminCreateUserRequest
        {
            Username = username,
            TemporaryPassword = password,
            UserPoolId = userPoolId,
            UserAttributes = attributeTypes
        };
        AdminCreateUserResponse adminCreateUserResponse = await adminAmazonCognitoIdentityProviderClient
            .AdminCreateUserAsync(adminCreateUserRequest);

        AdminUpdateUserAttributesRequest adminUpdateUserAttributesRequest = new AdminUpdateUserAttributesRequest
        {
            Username = username,
            UserPoolId = userPoolId,
            UserAttributes = new List<AttributeType>
                    {
                        new AttributeType()
                        {
                            Name = "email_verified",
                            Value = "true"
                        }
                    }
        };

        AdminUpdateUserAttributesResponse adminUpdateUserAttributesResponse = adminAmazonCognitoIdentityProviderClient
            .AdminUpdateUserAttributesAsync(adminUpdateUserAttributesRequest)
            .Result;


        AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = userPoolId,
            ClientId = appClientId,
            AuthFlow = "ADMIN_NO_SRP_AUTH",
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username},
                { "PASSWORD", password}
            }
        };

        AdminInitiateAuthResponse adminInitiateAuthResponse = await adminAmazonCognitoIdentityProviderClient
            .AdminInitiateAuthAsync(adminInitiateAuthRequest);

        AdminRespondToAuthChallengeRequest adminRespondToAuthChallengeRequest = new AdminRespondToAuthChallengeRequest
        {
            ChallengeName = ChallengeNameType.NEW_PASSWORD_REQUIRED,
            ClientId = appClientId,
            UserPoolId = userPoolId,
            ChallengeResponses = new Dictionary<string, string>
                    {
                        { "USERNAME", username },
                        { "NEW_PASSWORD", password }
                    },
            Session = adminInitiateAuthResponse.Session
        };

        AdminRespondToAuthChallengeResponse adminRespondToAuthChallengeResponse = await adminAmazonCognitoIdentityProviderClient
            .AdminRespondToAuthChallengeAsync(adminRespondToAuthChallengeRequest);
    }

    public async Task AdminAddUserToGroupAsync(
        string username,
        string userPoolId,
        string groupName)
    {
        AdminAddUserToGroupRequest adminAddUserToGroupRequest = new AdminAddUserToGroupRequest
        {
            Username = username,
            UserPoolId = userPoolId,
            GroupName = groupName
        };

        AdminAddUserToGroupResponse adminAddUserToGroupResponse = await adminAmazonCognitoIdentityProviderClient
            .AdminAddUserToGroupAsync(adminAddUserToGroupRequest);
    }

    public async Task<AdminInitiateAuthResponse> AdminAuthenticateUserAsync(
        string username,
        string password,
        string userPoolId,
        string appClientId)
    {
        AdminInitiateAuthRequest adminInitiateAuthRequest = new AdminInitiateAuthRequest
        {
            UserPoolId = userPoolId,
            ClientId = appClientId,
            AuthFlow = "ADMIN_NO_SRP_AUTH",
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username},
                { "PASSWORD", password}
            }
        };
        return await adminAmazonCognitoIdentityProviderClient
            .AdminInitiateAuthAsync(adminInitiateAuthRequest);
    }

    public async Task AdminRemoveUserFromGroupAsync(
        string username,
        string userPoolId,
        string groupName)
    {
        AdminRemoveUserFromGroupRequest adminRemoveUserFromGroupRequest = new AdminRemoveUserFromGroupRequest
        {
            Username = username,
            UserPoolId = userPoolId,
            GroupName = groupName
        };

        await adminAmazonCognitoIdentityProviderClient
            .AdminRemoveUserFromGroupAsync(adminRemoveUserFromGroupRequest);
    }

    public async Task AdminDisableUserAsync(
        string username,
        string userPoolId)
    {
        AdminDisableUserRequest adminDisableUserRequest = new AdminDisableUserRequest
        {
            Username = username,
            UserPoolId = userPoolId
        };

        await adminAmazonCognitoIdentityProviderClient
            .AdminDisableUserAsync(adminDisableUserRequest);
    }

    public async Task AdminDeleteUserAsync(
        string username,
        string userPoolId)
    {
        AdminDeleteUserRequest deleteUserRequest = new AdminDeleteUserRequest
        {
            Username = username,
            UserPoolId = userPoolId
        };

        await adminAmazonCognitoIdentityProviderClient
            .AdminDeleteUserAsync(deleteUserRequest);
    }
}