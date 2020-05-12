using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.Core.UserManagement.Dependency;
using ImpeccableService.Backend.Domain.UserManagement;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Utility.Application.ResultContract;

namespace ImpeccableService.Backend.Core.UserManagement
{
    internal class IdentitySecurityFactory
    {
        private readonly ISecurityEnvironmentVariables _securityEnvironmentVariables;

        public IdentitySecurityFactory(
            ISecurityEnvironmentVariables securityEnvironmentVariables)
        {
            _securityEnvironmentVariables = securityEnvironmentVariables;
        }

        public string HashPassword(string password)
        {
            var salt = Convert.FromBase64String(_securityEnvironmentVariables.PasswordHashSalt());

            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));

            return hashedPassword;
        }

        public async Task<ResultWithData<SecurityCredentials>> GenerateCredentials(User user)
        {
            var accessToken = await GenerateAccessToken(user);
            var refreshToken = GenerateToken();
            var logoutToken = GenerateToken();

            return new ResultWithData<SecurityCredentials>(new SecurityCredentials(accessToken, refreshToken, logoutToken));
        }

        private async Task<string> GenerateAccessToken(User user)
        {
            var secret = await _securityEnvironmentVariables.SecurityCredentialsSecret();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email) 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _securityEnvironmentVariables.SecurityCredentialsIssuer()
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateToken()
        {
            var randomNumber = new byte[64];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
