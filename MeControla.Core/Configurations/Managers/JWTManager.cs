using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeControla.Core.Configurations.Managers
{
    public class JWTManager : IJWTManager
    {
        private const string JWTREGISTEREDCLAIMNAMES_EMAIL = "email";

        private readonly IJWTConfiguration configuration;

        public JWTManager(IJWTConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IJWTData Generate(string username, string password, string accountId)
        {
            var (created, expiration) = GetDateTimeToExpire(configuration.TimeToExpire);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration.Issuer,
                Audience = configuration.Audience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iss, password),
                    new Claim(JwtRegisteredClaimNames.Sid, accountId),
                    new Claim(JWTREGISTEREDCLAIMNAMES_EMAIL, username)
                }),
                Expires = expiration,
                SigningCredentials = GetSigningCredentials(configuration.Secret)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JWTData
            {
                Token = tokenHandler.WriteToken(token),
                Jti = Guid.Parse(token.Id),
                Created = created,
                Expired = expiration
            };
        }

        private static (DateTime, DateTime) GetDateTimeToExpire(TimeSpan timeToExpire)
            => (DateTime.UtcNow, DateTime.UtcNow.Add(timeToExpire));

        private static SigningCredentials GetSigningCredentials(string secret)
            => new SigningCredentials(GetIssuerSigningKey(secret), SecurityAlgorithms.HmacSha256Signature);

        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out var validatedToken);
                return IsJwtWithValidSecurityAlgorithm(validatedToken)
                     ? principal
                     : null;
            }
            catch
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
            => validatedToken is JwtSecurityToken jwtSecurityToken
            && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        public TokenValidationParameters GetTokenValidationParameters()
            => new TokenValidationParameters
            {
                ValidateIssuerSigningKey = !string.IsNullOrWhiteSpace(configuration.Secret),
                IssuerSigningKey = GetIssuerSigningKey(configuration.Secret),
                ValidateIssuer = !string.IsNullOrWhiteSpace(configuration.Issuer),
                ValidateAudience = !string.IsNullOrWhiteSpace(configuration.Audience),
                ValidateLifetime = true,
                ValidAudience = configuration.Audience,
                ValidIssuer = configuration.Issuer,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };

        private static SymmetricSecurityKey GetIssuerSigningKey(string secret)
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }
}