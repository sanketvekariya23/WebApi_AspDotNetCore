using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login_Registor.Providers
{
    public class AccessProviders
    {
        public enum SystemUserType { Admin , User}
        public static string GetUserAccessToken(object users, DateTime validityTill, Claim additionalClaim = null)
        {
            byte[] AuthKey = Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey);
            JwtSecurityTokenHandler tokenHandler = new();
            List<Claim> claims = new() { new Claim(ClaimTypes.UserData, users.ToJson()) };
            if (additionalClaim != null) { claims.Add(additionalClaim); }
            return tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = validityTill,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(AuthKey),
                    SecurityAlgorithms.HmacSha256Signature)
            }));
        }
    }
}
