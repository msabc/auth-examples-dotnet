namespace AuthenticationExamples.Domain.Models.Tables
{
    public record Role
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
