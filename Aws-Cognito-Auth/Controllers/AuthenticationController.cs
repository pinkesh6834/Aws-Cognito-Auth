using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Aws_Cognito_Auth.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private const string _clientId = "3smco4e3im3euehmedtluq0bui";
        private readonly RegionEndpoint _region = RegionEndpoint.USWest2;

        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<ActionResult<string>> Register(User user)
        {
            var cognito = new AmazonCognitoIdentityProviderClient(_region);

            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Password = user.Password,
                Username = user.Username
            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };

            signUpRequest.UserAttributes.Add(emailAttribute);

            try
            {
                var response = await cognito.SignUpAsync(signUpRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }

        }

        [HttpPost]
        [Route("api/signin")]
        public async Task<ActionResult<string>> SignIn(User user)
        {
            var cognito = new AmazonCognitoIdentityProviderClient(_region);

            var request = new AdminInitiateAuthRequest
            {
                UserPoolId = "us-west-2_EOGDtXwex",
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };

            request.AuthParameters.Add("USERNAME", user.Username);
            request.AuthParameters.Add("PASSWORD", user.Password);

            try
            {
                var response = await cognito.AdminInitiateAuthAsync(request);
                return Ok(response.AuthenticationResult.IdToken);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}