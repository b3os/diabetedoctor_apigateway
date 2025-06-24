using System.Security.Claims;

namespace ApiGateway.Api.Common.Constants;

public sealed class AuthConstants
{
    public const string BearerTokenScheme = "Bearer";
    public const string AccessToken = "AccessToken";
    public const string UserId = "UserId";
    public const string Role = ClaimTypes.Role;
}

