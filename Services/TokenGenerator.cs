using Microsoft.IdentityModel.Tokens;
using StackOverflow.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StackOverflow.Services
{
    public static class TokenGenerator
    {
        private static readonly string s_appSecret =
        "Man's already gone crazy."
        + "Ma gasesti in masina"
        + "Amesteci sprite cu codeina"
        + "Vedeta rockstar"
        + "Prin ochelarii mei versace, te"
        + "Vad atat de clar"
        + "Majin Vegeta, baby Vegeta final flash"
        + "Dau foc la pai cu bricheta"
        + "Ti-ai lipit de fata levierul"
        + "Ti-am zis c-o sa cunosti fieru"
        + "Yo soyu no jitano"
        + "Rumeno no italiano"
        + "Toti peste unu si zid peste toti"
        + "Fereste-te de Exodia daca poti"
        + "Nu te tin legat obligat"
        + "Te tin cu catusa si pistol la cap"
        + "Iti dau clasa-n trap"
        + "Arata-ne unde-ti sunt c**** e ca noi n-am inteles."
        + "Ti-o rugini coroana de * inaudible*"
        + "Sclavii sa m-atace atata timp cat ceea ce fac"
        + "Nu le place."
        + "Inot in sirop"
        + "Ma inec si ma-ntorc"
        + "Ma inec in sirop"
        + "Plec si ma-ntorc"
        + "In fata la bloc"
        + "Inot in sirop"
        + "Ma inec si ma-ntorc"
        + "In fata la bloc."
        + "INOT IN SIROOP"
        + "MA INEC SI MA-NTORC"
        + "MA INEC IN SIROP"
        + "PLEC SI MA-NTORC"
        + "IN FATA LA BLOC"
        + "MA INEC IN SIROP"
        + "MA INEC IN SIROP"
        + "IN FATA LA BLOC."
        + "Nu-s bataus de prima clasa"
        + "Sunt un slabanog"
        + "Care intra"
        + "Cu gangu'"
        + "La tine-n casa"
        + "Ochelarii mei Versace niciodata"
        + "Nu m-au dezamagit"
        + "De fetele tarfelor cu doua fete m-au ferit"
        + "Baga iarba si money"
        + "In chilotii mei Armani"
        + "Scoate treaba aia din frigider"
        + "Calitate bro"
        + "Filmul meu nu mai e la fel"
        + "Pachetele, pachetele"
        + "Pana facem o avere"
        + "Vine garda dupa mine"
        + "Eu fug din scara-n scara"
        + "Fug de gabori idioti seara de seara"
        + "Alo de ma cauta zile cand n-am mai dat pe-acasa domnisoara"
        + "Uite m-am intalnit cu Mateo"
        + "Iti aduce marfa aia"
        + "Suntem pusi pe treaba"
        + "Sa facem dinero"
        + "(Unu, unu, unu, unu, unuuuuu)"
        + "Sa facem dinerooo."
        + "Inot in sirop"
        + "Ma inec si ma-ntorc"
        + "Ma inec in sirop"
        + "Plec si ma-ntorc"
        + "In fata la bloc"
        + "Inot in sirop"
        + "Ma inec si ma-ntorc"
        + "In fata la bloc."
        + "INOT IN SIROP"
        + "MA INEC SI MA-NTORC"
        + "MA INEC IN SIROP"
        + "PLEC SI MA INTORC"
        + "IN FATA LA BLOC"
        + "MA INEC IN SIROP"
        + "MA INEC IN SIROP"
        + "IN FATA LA BLOC";

        public static string Generate(ApplicationUser user)
        {
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s_appSecret)), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }   
    }
}
