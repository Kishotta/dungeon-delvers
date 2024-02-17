namespace CharacterManagement.Api.Options;

public class IdentityProviderOptions
{
    public static string Section => "IdentityProvider";

    public string Authority { get; set; } = string.Empty;
    public string Issuer    { get; set; } = string.Empty;
    public string Audience  { get; set; } = string.Empty;
}
