using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using POD.API.User.Identity.Data.Database;
using POD.API.User.Identity.Services.Interfaces;
using POD.Common.Database.Models;

namespace POD.API.User.Identity.Services
{
    public class IdentityService(
        ITokenService tokenService,
        IdentityContext dbContext,
        IEmailService emailService) : IIdentityService
    {
        public async Task<bool> VerifyAndProcess(string code, bool update = false)
        {
            var jObject = update
                ? await tokenService.ExchangeCodeForTokenAsync(code, true)
                : await tokenService.ExchangeCodeForTokenAsync(code);
            
            if (jObject == null)
            {
                return false;
            }

            var idToken = jObject.Value<string>("id_token");
            
            if (string.IsNullOrEmpty(idToken))
            {
                return false;
            }
            
            var user = DecodeIdToken(idToken);

            if (update)
            {
                await UpdateExistingUser(user);
            }
            else
            {
                await CreateNewUser(user);
            }

            return true;
        }
        
        // PRIVATE METHODS

        private POD.API.User.Identity.Data.Models.User DecodeIdToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken) as JwtSecurityToken;

            if (jsonToken == null)
            {
                throw new ArgumentException("Invalid token.");
            }

            var payload = jsonToken.Payload;

            var emailsJson = payload["emails"].ToString();
            string firstEmail = string.Empty;

            if (!string.IsNullOrEmpty(emailsJson))
            {
                try
                {
                    var emailsArray = JsonSerializer.Deserialize<string[]>(emailsJson);
                    firstEmail = emailsArray?.FirstOrDefault() ?? string.Empty;
                }
                catch (JsonException)
                {
                    throw new ArgumentException("Invalid token.");
                }
            }

            return new POD.API.User.Identity.Data.Models.User
            {
                FamilyName = payload["family_name"].ToString() ?? string.Empty,
                GivenName = payload["given_name"].ToString() ?? string.Empty,
                OID = payload["oid"].ToString() ?? string.Empty,
                Email = firstEmail,
            };
        }

        private async Task CreateNewUser(POD.API.User.Identity.Data.Models.User user)
        {
            var doesUserExists = await dbContext.Users
                .AnyAsync(u => u.IsEnabled == true && u.RefId == Guid.Parse(user.OID));

            if (doesUserExists)
            {
                return;
            }
            
            await dbContext.Users.AddAsync(new Common.Database.Models.User
            {
                RefId = Guid.Parse(user.OID),
                FirstName = user.GivenName,
                LastName = user.FamilyName,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Email = user.Email,
                Phone = "to be provided",
                IsPrivacyPolicyAccepted = false,
                HasTakenTour = false,
                Sellers = new HashSet<Seller>
                {
                    new Seller
                    {
                        Discount = 0,
                        HasBeenUpdated = false,
                        IsEnabled = true,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        Status = "OK",
                        UserRefId = Guid.Parse(user.OID)
                    }
                }
            });

            await dbContext.SaveChangesAsync();

            await emailService.SendWelcomeEmail(user.Email, user.GivenName, user.FamilyName);
        }

        private async Task UpdateExistingUser(POD.API.User.Identity.Data.Models.User user)
        {
            var existingUser = await dbContext.Users
                .FirstOrDefaultAsync(u => u.IsEnabled == true && u.RefId == Guid.Parse(user.OID));

            if (existingUser == null)
            {
                return;
            }

            existingUser.FirstName = user.GivenName;
            existingUser.LastName = user.FamilyName;

            await dbContext.SaveChangesAsync();

            await emailService.SendProfileUpdatedEmail(user.Email, user.GivenName, user.FamilyName);
        }
    }
}