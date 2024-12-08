namespace AuthenticationExamples.Configuration
{
    public record DatabaseSettings
    {
        public required string ConnectionString { get; set; }
    }
}
