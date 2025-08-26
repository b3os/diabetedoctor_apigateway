namespace ApiGateway.Api.Settings;
public class AuthSettings
{
    public const string SectionName = "AuthSettings";
    public string AccessSecretToken { get; init; } = null!;
    public string AccessTokenExpMinute { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
}