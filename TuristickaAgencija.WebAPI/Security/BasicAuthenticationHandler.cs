using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using TuristickaAgencija.Services.Korisnici;

namespace TuristickaAgencija.WebAPI.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "BasicAuthentication";

        private readonly IKorisniciService _korisniciService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IKorisniciService korisniciService)
            : base(options, logger, encoder)
        {
            _korisniciService = korisniciService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.NoResult();
            }

            string username;
            string password;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (!"Basic".Equals(authHeader.Scheme, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(authHeader.Parameter))
                {
                    return AuthenticateResult.Fail("Neispravno Authorization zaglavlje.");
                }

                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter));
                var separator = credentials.IndexOf(':');
                if (separator < 0)
                {
                    return AuthenticateResult.Fail("Neispravno Authorization zaglavlje.");
                }

                username = credentials[..separator];
                password = credentials[(separator + 1)..];
            }
            catch
            {
                return AuthenticateResult.Fail("Neispravno Authorization zaglavlje.");
            }

            var user = await _korisniciService.AuthenticateAsync(username, password);
            if (user == null)
            {
                return AuthenticateResult.Fail("Pogrešno korisničko ime ili lozinka.");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.KorisnickoIme),
                new Claim(ClaimTypes.GivenName, user.Ime ?? string.Empty)
            };
            claims.AddRange(user.Uloge.Select(uloga => new Claim(ClaimTypes.Role, uloga)));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static int KorisnikId(this ClaimsPrincipal user)
        {
            var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(value, out var id) ? id : 0;
        }

        public static bool JeAdmin(this ClaimsPrincipal user) => user.IsInRole(Model.UlogeNazivi.Admin);
    }
}
