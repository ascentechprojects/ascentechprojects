using app.advertise.dtos.Identity;
using app.advertise.libraries.AppSettings;
using app.advertise.services.Auth.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace app.advertise.services.Auth.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly JwtDefaultSetting _jwtDefaultSettings;
        private readonly ILogger<JwtSecurityToken> _logger;

        public TokenService(IOptions<JwtDefaultSetting> jwtDefaultSettings, ILogger<JwtSecurityToken> logger)
        {
            _jwtDefaultSettings = jwtDefaultSettings.Value;
            _logger = logger;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(25),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtDefaultSettings.SecretKey)), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);
            return token;
        }

        public RefreshToken GenerateRefreshToken(string userId)
        {
            // generate token that is valid for 7 days
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            //var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            //TODO - Expires duration add from json file
            var refreshToken = new RefreshToken
            {
                //ApplicationUserId = userId,
                Token = Convert.ToBase64String(randomBytes),
                //Expires = DateTime.UtcNow.AddDays(1),
                //Created = DateTime.UtcNow,
                //CreatedByIp = ipAddress,                
            };

            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtDefaultSettings.SecretKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }

        public void RevokeAccessToken()
        {

        }
    }
}
