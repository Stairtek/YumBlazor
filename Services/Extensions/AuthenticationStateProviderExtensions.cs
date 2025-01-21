using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace YumBlazor.Services.Extensions;

public static class AuthenticationStateProviderExtensions
{
    public static async Task<UserAuthentication> GetUserAuthentication(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authenticationState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var userPrincipal = authenticationState.User;
        var userAuthentication = new UserAuthentication
        {
            IsAuthenticated = userPrincipal.Identity?.IsAuthenticated ?? false,
            UserId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
        };
        
        return userAuthentication;
    }
}


public class UserAuthentication
{
    public string? UserId { get; set; }

    public bool IsAuthenticated { get; set; }
}