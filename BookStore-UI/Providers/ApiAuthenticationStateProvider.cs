using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore_UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        
        private readonly ILocalStorageService _localStorage;

        private readonly JwtSecurityTokenHandler _tokenHandler;

        public ApiAuthenticationStateProvider( ILocalStorageService localStorage,
            JwtSecurityTokenHandler tokenHandler )
        {
            
            _localStorage = localStorage;
            _tokenHandler = tokenHandler;
        }

        /// <summary>
        /// Need to revisit this code, is potentialy unsecure, by storing token in browser,
        /// </summary>
        /// <returns></returns>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var savedToken = await _localStorage.GetItemAsync<string>("authToken");
                if(string.IsNullOrWhiteSpace(savedToken))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsPrincipal()));
                }
                var tokenContent = _tokenHandler.ReadJwtToken(savedToken);
                var expiery = tokenContent.ValidTo;
                if(expiery < DateTime.Now)
                {
                    await _localStorage.RemoveItemAsync("authToken");
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsPrincipal()));
                }

                //Get claim from token and Build auth user object
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                // return atuhenicated person

                return new AuthenticationState(user);

            }
            catch (Exception)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsPrincipal()));
            }

        }
        public async Task LoggedIn()
        {
            var savedToken = await _localStorage.GetItemAsync<string>("authToken");
            var tokenContent = _tokenHandler.ReadJwtToken(savedToken);
            var claims = ParseClaims(tokenContent);
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }
        public void LoggedOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsPrincipal());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
                

        }
            private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
        {
            var claims = tokenContent.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
            return claims;
        }
    }
}
