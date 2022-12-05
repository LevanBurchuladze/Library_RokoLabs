
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.UI.WebApi.infrastructures
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUEDIENCE = "MyAuthClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
