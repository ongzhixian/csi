using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Csi.Authentication
{
    public interface ITokenAuthentication
    {
        string GetToken(string username, string password);

        
    }
    public class JwtAuthenticator : ITokenAuthentication
    {
        string issuer = "http://mysite.com";
        string secret = "TODO:ToReplaceLater123";

        private JwtSecurityTokenHandler securityTokenHandler = null;
        private SymmetricSecurityKey securityKey = null;
        private SigningCredentials signingCredentials = null;

        public JwtAuthenticator()
        {
            this.securityTokenHandler = new JwtSecurityTokenHandler();

            this.securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            this.signingCredentials = new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public string GetToken(string username, string password)
        {
            
            var myAudience = "http://myaudience.com";
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = this.issuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = this.securityTokenHandler.CreateToken(tokenDescriptor);
	        
            return this.securityTokenHandler.WriteToken(securityToken);
            
        }
    }
}
