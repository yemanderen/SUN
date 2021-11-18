using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CDBWebSecurity.Models;
using Exceptionless;
using Exceptionless.Json;
using Microsoft.IdentityModel.Tokens;


namespace CDBWebSecurity.Services
{
    public class PingAccessService
    {

        public static bool ValidateUserToken(string token)
        {
            var success = true;
            var jwtArr = token.Split('.');
            var header = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, object>>(Base64UrlEncoder.Decode(jwtArr[1]));

            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(Const.securityKey));
            success = string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            return success;
        }

        public static string CreateJwtToken(string userName)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(300)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, userName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jtwToken = new JwtSecurityToken(
                issuer: Const.issuer,
                audience: Const.audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(300),
                signingCredentials: creds);

            string strToken = new JwtSecurityTokenHandler().WriteToken(jtwToken);
            return strToken;
        }

    }
}
