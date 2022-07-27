namespace SE.Identity.API.Extensions
{
    public class AppSettings
    {
        public int ExpirationHours { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}