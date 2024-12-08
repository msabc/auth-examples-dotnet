namespace AuthenticationExamples.Configuration
{
    public record BasicAuthSettings
    {
        public required string BasicAuthToken { get; set; }
    }
}
