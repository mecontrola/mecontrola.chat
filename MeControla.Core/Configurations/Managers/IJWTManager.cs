using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MeControla.Core.Configurations.Managers
{
    public interface IJWTManager
    {
        IJWTData Generate(string username, string password, string accountId);
        ClaimsPrincipal GetClaimsPrincipal(string token);
        TokenValidationParameters GetTokenValidationParameters();
    }
}