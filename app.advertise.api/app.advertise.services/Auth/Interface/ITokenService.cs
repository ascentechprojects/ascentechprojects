using app.advertise.dtos.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace app.advertise.services.Auth.Interface
{
        public interface ITokenService
        {
            string GenerateAccessToken(IEnumerable<Claim> claims);
            ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
            RefreshToken GenerateRefreshToken(string userId);

        }
}
