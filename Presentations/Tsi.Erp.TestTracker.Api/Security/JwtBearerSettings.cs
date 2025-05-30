namespace Tsi.Erp.TestTracker.Api.Security
{
    /// <summary>
    /// To use this configuration create a section on appsettings.json with all properties in <see cref="JwtBearerSettings" />
    /// </summary>
    public class JwtBearerSettings
    {
        public string ValidAudience { get; set; }=string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
