using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(_anonymous));
    }

    public void SetAuthenticationState(Task<AuthenticationState> authenticationStateTask)
    {
        _anonymous = authenticationStateTask.Result.User;
        NotifyAuthenticationStateChanged(authenticationStateTask);
    }

}

