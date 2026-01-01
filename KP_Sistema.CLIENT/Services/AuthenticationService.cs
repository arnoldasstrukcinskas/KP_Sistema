using KP_Sistema.CONTRACTS.DTO.AuthenticationDTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Text.Json;

namespace KP_Sistema.CLIENT.Services
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationService(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Get user data from localStorage (saved by JS)
            var userJson = await _js.InvokeAsync<string>("auth.getUserData");

            if (string.IsNullOrEmpty(userJson))
            {
                // No user data, return anonymous
                return new AuthenticationState(_anonymous);
            }

            // Convert JSON into LoginResponseDTO
            var loginData = JsonSerializer.Deserialize<LoginResponseDTO>(userJson);

            if (loginData == null || loginData.user == null)
                return new AuthenticationState(_anonymous);

            // Create claims based on user info
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginData.user.Id.ToString()),
                new Claim(ClaimTypes.Name, loginData.user.Username)
            };

            // Add role claim if present
            if (!string.IsNullOrEmpty(loginData.user.Role))
                claims.Add(new Claim(ClaimTypes.Role, loginData.user.Role));

            // Create identity and principal
            var identity = new ClaimsIdentity(claims, "apiauth");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        // Call this when user logs in to notify Blazor
        public void NotifyUserAuthentication(LoginResponseDTO loginData)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginData.user.Id.ToString()),
                new Claim(ClaimTypes.Name, loginData.user.Username)
            };

            if (!string.IsNullOrEmpty(loginData.user.Role))
                claims.Add(new Claim(ClaimTypes.Role, loginData.user.Role));

            var identity = new ClaimsIdentity(claims, "apiauth");
            var user = new ClaimsPrincipal(identity);

            // Notify Blazor that authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        // Call this when user logs out
        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
        }
    }
}
