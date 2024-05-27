using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace FitnessAppAPI.Services
{
    public class AuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UsersService _usersService;

        public AuthenticationService(AuthenticationStateProvider authenticationStateProvider, UsersService usersService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _usersService = usersService;
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await _usersService.GetEntryByEmail(email);

            if (user == null || user.password != password)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user._id ?? string.Empty),
                new Claim(ClaimTypes.Name, user.name ?? string.Empty),
                new Claim("CNP", user.CNP ?? string.Empty),
                new Claim(ClaimTypes.Email, user.email ?? string.Empty),
                new Claim("Phone", user.phone ?? string.Empty),
                new Claim("Address", user.address ?? string.Empty),
                new Claim(ClaimTypes.Role, user.admin == true ? "admin" : "user"),

            };

            var identity = new ClaimsIdentity(claims, "apiauth_type");
            var userPrincipal = new ClaimsPrincipal(identity);

            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetAuthenticationState(Task.FromResult(new AuthenticationState(userPrincipal)));

            return true;
        }

        public void Logout()
        {
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetAuthenticationState(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }
    }
}