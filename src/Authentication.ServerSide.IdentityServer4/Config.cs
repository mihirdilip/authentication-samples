using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace Authentication.ServerSide
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> Ids =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile()
			};


		public static IEnumerable<ApiResource> Apis =>
			new List<ApiResource>
			{
				new ApiResource("api", "My API") { Scopes = { new Scope(OidcConstants.StandardScopes.OfflineAccess) } }
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				// console  client
				new Client
				{
					ClientId = "console-client",
					ClientSecrets = { new Secret("console-client-secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

					AllowOfflineAccess = true,
					
					AccessTokenLifetime = 5,

					// scopes that client has access to
					AllowedScopes = { "api" }
				},

				// machine to machine client
				new Client
				{
					ClientId = "api-client",
					ClientSecrets = { new Secret("api-client-secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.ClientCredentials,
					
					// scopes that client has access to
					AllowedScopes = { "api" }
				},

				// interactive ASP.NET Core MVC client
				new Client
				{
					ClientId = "mvc-client",
					ClientSecrets = { new Secret("mvc-client-secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,
					RequireConsent = false,
					RequirePkce = true,
                
					// where to redirect to after login
					RedirectUris = { "http://localhost:5002/signin-oidc" },

					// where to redirect to after logout
					PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile
					}
				}
			};
	}

	public class TestUsers
	{
		public static List<TestUser> Users = new List<TestUser>
		{
			new TestUser{SubjectId = "818727", Username = "alice", Password = "alice",
				Claims =
				{
					new Claim(JwtClaimTypes.Name, "Alice Smith"),
					new Claim(JwtClaimTypes.GivenName, "Alice"),
					new Claim(JwtClaimTypes.FamilyName, "Smith"),
					new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
					new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
					new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
					new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
				}
			},
			new TestUser{SubjectId = "88421113", Username = "bob", Password = "bob",
				Claims =
				{
					new Claim(JwtClaimTypes.Name, "Bob Smith"),
					new Claim(JwtClaimTypes.GivenName, "Bob"),
					new Claim(JwtClaimTypes.FamilyName, "Smith"),
					new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
					new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
					new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
					new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json),
					new Claim("location", "somewhere")
				}
			}
		};
	}
}
