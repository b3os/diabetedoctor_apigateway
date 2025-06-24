namespace ApiGateway.Api.Settings;
public class AuthSettings
{
    public const string SectionName = "AuthSettings";
    public string AccessSecretToken { get; set; } = default!;
    public string AccessTokenExpMinute { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
}